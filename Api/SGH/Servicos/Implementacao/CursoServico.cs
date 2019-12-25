using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Dominio.Model;
using Dominio.ViewModel;
using Global;
using Repositorio;
using Servico.Contratos;

namespace Servico.Implementacao
{
    public class CursoServico : ICursoService
    {
        private readonly IRepositorio<Curso> _repositorio;
        private readonly IMapper _mapper;

        public CursoServico(IRepositorio<Curso> repositorio, IMapper mapper)
        {
            _repositorio = repositorio;
            _mapper = mapper;
        }

        public async Task<Resposta<CursoViewModel>> Atualizar(CursoViewModel entidadeViewModel)
        {
            try
            {
                var entidade = _mapper.Map<Curso>(entidadeViewModel);
                var resultado = _mapper.Map<CursoViewModel>(await _repositorio.Atualizar(entidade));
                return new Resposta<CursoViewModel>(resultado);
            }
            catch (Exception e)
            {
                return new Resposta<CursoViewModel>(entidadeViewModel, $"Ocorreu um erro ao atualizar o curso: {e.Message}");
            }
        }

        public async Task<Resposta<CursoViewModel>> Criar(CursoViewModel entidade)
        {
            try
            {
                var resultado = await _repositorio.Criar(_mapper.Map<Curso>(entidade));
                return new Resposta<CursoViewModel>(_mapper.Map<CursoViewModel>(resultado));
            }
            catch (Exception e)
            {
                return new Resposta<CursoViewModel>(entidade, $"Ocorreu um erro ao criar o curso: {e.Message}");
            }
        }

        public async Task<Resposta<Paginacao<CursoViewModel>>> ListarComPaginacao(Paginacao<CursoViewModel> entidade)
        {
            try
            {
                var resultado = await _repositorio.ListarPorPaginacao(_mapper.Map<Paginacao<Curso>>(entidade));
                if (resultado.TemErro())
                    return new Resposta<Paginacao<CursoViewModel>>(null, resultado.GetErros());

                var resultadoViewModel = _mapper.Map<Paginacao<CursoViewModel>>(resultado.GetResultado());

                return new Resposta<Paginacao<CursoViewModel>>(resultadoViewModel);
            }
            catch (Exception e)
            {
                return new Resposta<Paginacao<CursoViewModel>>(null, $"Ocorreu um erro ao listar o curso: {e.Message}");
            }
        }

        public async Task<Resposta<List<CursoViewModel>>> ListarTodos()
        {
            var resultado = await _repositorio.ListarTodos();
            return new Resposta<List<CursoViewModel>>(_mapper.Map<List<CursoViewModel>>(resultado));
        }

        public async Task<Resposta<bool>> Remover(long id)
        {
            try
            {
                var result = await _repositorio.Remover(lnq => lnq.Codigo == id);

                if (result)
                    return new Resposta<bool>(result);

                return new Resposta<bool>(false, $"Não foi possível remover o curso!");
            }
            catch (Exception e)
            {
                return new Resposta<bool>(false, $"Não foi possível remover o curso: {e.Message}!");
            }
        }
    }
}
