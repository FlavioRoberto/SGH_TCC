using AutoMapper;
using Dominio.Model.DisciplinaModel;
using Dominio.ViewModel;
using Dominio.ViewModel.DisciplinaViewModel;
using Global;
using Repositorio;
using Aplicacao.Contratos;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Aplicacao.Implementacao.DisciplinaImp
{
    public class DisciplinaServico : IDisciplinaService
    {
        private readonly IRepositorio<Disciplina> _repositorio;
        private readonly IMapper _mapper;

        public DisciplinaServico(IRepositorio<Disciplina> repositorio, IMapper mapper)
        {
            _repositorio = repositorio;
            _mapper = mapper;
        }

        public async Task<Resposta<Paginacao<DisciplinaViewModel>>> ListarComPaginacao(Paginacao<DisciplinaViewModel> entidade)
        {
            try
            {
                var resultado = await _repositorio.ListarPorPaginacao(_mapper.Map<Paginacao<Disciplina>>(entidade));
                if (resultado.TemErro())
                    return new Resposta<Paginacao<DisciplinaViewModel>>(null, resultado.GetErros());

                var resultadoViewModel = _mapper.Map<Paginacao<DisciplinaViewModel>>(resultado.GetResultado());

                return new Resposta<Paginacao<DisciplinaViewModel>>(resultadoViewModel);
            }
            catch (Exception e)
            {
                return new Resposta<Paginacao<DisciplinaViewModel>>(null, $"Ocorreu um erro ao listar a disciplina: {e.Message}");
            }
        }

        public async Task<Resposta<List<DisciplinaViewModel>>> ListarTodos()
        {
            var resultado = await _repositorio.ListarTodos();
            return new Resposta<List<DisciplinaViewModel>>(_mapper.Map<List<DisciplinaViewModel>>(resultado));
        }

        public async Task<Resposta<List<DisciplinaViewModel>>> listarPorDescricao(string descricao)
        {
            try
            {
                var dados = await _repositorio.ListarPor(lnq => lnq.Descricao.ToLower().Contains(descricao.ToLower()));

                if (dados == null)
                    return new Resposta<List<DisciplinaViewModel>>(new List<DisciplinaViewModel>());

                var dadosViewModel = _mapper.Map<List<DisciplinaViewModel>>(dados);
                return new Resposta<List<DisciplinaViewModel>>(dadosViewModel);
            }
            catch (Exception e)
            {
                return new Resposta<List<DisciplinaViewModel>>(null, "Ocorreu um erro ao listar as disciplinas: " + e.Message);
            }

        }

    }
}
