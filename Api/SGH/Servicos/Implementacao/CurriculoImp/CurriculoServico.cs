using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Dominio.Model;
using Dominio.ViewModel;
using Dominio.ViewModel.CurriculoViewModel;
using Global;
using Repositorio;
using Servico.Contratos;

namespace Servico.Implementacao.CurriculoImp
{
    public class CurriculoServico : ICurriculoService
    {
        private IRepositorio<Curriculo> _repositorio;
        private IMapper _mapper;

        public CurriculoServico(IRepositorio<Curriculo> repositorio, IMapper mapper)
        {
            _repositorio = repositorio;
            _mapper = mapper;
        }

        public async Task<Resposta<CurriculoViewModel>> Atualizar(CurriculoViewModel entidadeViewModel)
        {
            try
            {
                var entidade = _mapper.Map<Curriculo>(entidadeViewModel);
                var resultado = _mapper.Map<CurriculoViewModel>(await _repositorio.Atualizar(entidade));
                resultado.Disciplinas = resultado.Disciplinas.OrderBy(lnq => lnq.Periodo).ToList();
                return new Resposta<CurriculoViewModel>(resultado);
            }
            catch (Exception e)
            {
                return new Resposta<CurriculoViewModel>(entidadeViewModel, $"Ocorreu um erro ao atualizar o currículo: {e.Message}");
            }
        }

        public async Task<Resposta<CurriculoViewModel>> Criar(CurriculoViewModel viewModel)
        {
            try
            {
                string mensagemErro = ValidarDadosCurriculo(viewModel);

                if (!string.IsNullOrEmpty(mensagemErro))
                    return new Resposta<CurriculoViewModel>(null, $"Não foi possível cadastrar um novo currículo: s{mensagemErro}");
                
                var entidade = _mapper.Map<Curriculo>(viewModel);

               // entidade.Disciplinas = _mapper.Map<List<CurriculoDisciplina>>(viewModel.Disciplinas);

                var resultado = await _repositorio.Criar(entidade);
                var mapeamento = new Resposta<CurriculoViewModel>(_mapper.Map<CurriculoViewModel>(resultado));
                return mapeamento;
            }
            catch (Exception e)
            {
                return new Resposta<CurriculoViewModel>(null, $"Ocorreu um erro ao salvar o currículo: {e.Message}");
            }
        }

        public async Task<Resposta<Paginacao<CurriculoViewModel>>> ListarComPaginacao(Paginacao<CurriculoViewModel> entidade)
        {
            try
            {
                var resultado = await _repositorio.ListarPorPaginacao(_mapper.Map<Paginacao<Curriculo>>(entidade));

                if (resultado.TemErro())
                    return new Resposta<Paginacao<CurriculoViewModel>>(null, resultado.GetErros());

                var resultadoViewModel = _mapper.Map<Paginacao<CurriculoViewModel>>(resultado.GetResultado());

                return new Resposta<Paginacao<CurriculoViewModel>>(resultadoViewModel);
            }
            catch (Exception e)
            {
                return new Resposta<Paginacao<CurriculoViewModel>>(null, $"Ocorreu um erro ao listar o currículo: {e.Message}");
            }
        }

        public Task<Resposta<CurriculoViewModel>> ListarPeloId(long id)
        {
            throw new System.NotImplementedException();
        }

        public Task<Resposta<List<CurriculoViewModel>>> ListarTodos()
        {
            throw new System.NotImplementedException();
        }

        public async Task<Resposta<bool>> Remover(long id)
        {
            try
            {                
                var result = await _repositorio.Remover(lnq => lnq.Codigo == id);

                if (result)
                    return new Resposta<bool>(result);
                else
                    return new Resposta<bool>(false, $"Não foi possível remover o currículo: Currículos com código {id} não encontrado!");
            }
            catch (Exception e)
            {
                return new Resposta<bool>(false, $"Não foi possível remover o currículo: {e.Message}");
            }
        }

        private string ValidarDadosCurriculo(CurriculoViewModel curriculo)
        {
            var mensagem = new StringBuilder();


            if(curriculo.CodigoCurso <= 0)
                mensagem.Append("Código do curso não foi informado!");

            if (curriculo.CodigoTurno <= 0)
                mensagem.Append("Código do turno não foi informado!");

            if (curriculo.CodigoTurno <= 0)
                mensagem.Append("Período não foi informado!");

            if(curriculo.Ano <= 0)
                mensagem.Append("Ano não foi informado!");

            if (curriculo.Disciplinas.Count() <= 0)
            {
                mensagem.Append("Disciplinas não foram informadas!");
                return mensagem.ToString();
            }

            return mensagem.ToString();
        }
    }
}
