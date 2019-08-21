using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Dominio.Model;
using Dominio.Model.CurriculoModel;
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

        public Task<Resposta<CurriculoViewModel>> Atualizar(CurriculoViewModel entidade)
        {
            throw new System.NotImplementedException();
        }

        public async Task<Resposta<CurriculoViewModel>> Criar(CurriculoViewModel viewModel)
        {
            try
            {
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

        public Task<Resposta<Paginacao<CurriculoViewModel>>> ListarComPaginacao(Paginacao<CurriculoViewModel> entidade)
        {
            throw new System.NotImplementedException();
        }

        public Task<Resposta<CurriculoViewModel>> ListarPeloId(long id)
        {
            throw new System.NotImplementedException();
        }

        public Task<Resposta<List<CurriculoViewModel>>> ListarTodos()
        {
            throw new System.NotImplementedException();
        }

        public Task<Resposta<bool>> Remover(long id)
        {
            throw new System.NotImplementedException();
        }
    }
}
