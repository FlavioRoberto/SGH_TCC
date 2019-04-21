using System;
using System.Collections.Generic;
using AutoMapper;
using Dominio.Model;
using Dominio.ViewModel;
using Global;
using Repositorio;
using Servico.Contratos;

namespace Servico.Implementacao
{
    public class CursoServico : ICursoServico
    {
        private readonly IRepositorio<Curso> _repositorio;
        private readonly IMapper _mapper;

        public CursoServico(IRepositorio<Curso> repositorio, IMapper mapper)
        {
            _repositorio = repositorio;
            _mapper = mapper;
        }

        public Resposta<CursoViewModel> Atualizar(CursoViewModel entidadeViewModel)
        {
            try
            {
                var entidade = _mapper.Map<Curso>(entidadeViewModel);
                var resultado = _mapper.Map<CursoViewModel>(_repositorio.Atualizar(entidade).Result);
                return new Resposta<CursoViewModel>(resultado);
            }
            catch (Exception e)
            {
                return new Resposta<CursoViewModel>(entidadeViewModel, "Ocorreu um erro ao atualizar o curso: " + e.Message);
            }
        }

        public Resposta<Paginacao<CursoViewModel>> ListarComPaginacao(Paginacao<CursoViewModel> entidade)
        {
            try
            {
                var resultado = _repositorio.ListarPorPaginacao(_mapper.Map<Paginacao<Curso>>(entidade));
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

        public Resposta<CursoViewModel> Criar(CursoViewModel entidade)
        {
            try
            {
                var resultado = _repositorio.Criar(_mapper.Map<Curso>(entidade)).Result;
                return new Resposta<CursoViewModel>(_mapper.Map<CursoViewModel>(resultado));
            }
            catch (Exception e)
            {
                return new Resposta<CursoViewModel>(entidade,"Ocorreu um erro ao criar o curso: "+e.Message);
            }
        }

        public Resposta<CursoViewModel> ListarPeloId(long id)
        {
            try
            {
                var resultado = _repositorio.Listar(lnq => lnq.Codigo == id).Result;
                return new Resposta<CursoViewModel>(_mapper.Map<CursoViewModel>(resultado));
            }
            catch (Exception e)
            {
                return new Resposta<CursoViewModel>(null, $"Ocorreu um erro ao listar o curso pelo id: {id}: " + e.Message);
            }
        }

        public Resposta<List<CursoViewModel>> ListarTodos()
        {
            try
            {
                var resultado = _repositorio.ListarTodos().Result;
                return new Resposta<List<CursoViewModel>>(_mapper.Map<List<CursoViewModel>>(resultado));
            }
            catch (Exception e)
            {
                return new Resposta<List<CursoViewModel>>(null, "Ocorreu um erro ao listar os cursos: " + e.Message);
            }
        }

        public Resposta<bool> Remover(long id)
        {
            try
            {
                var resultado = _repositorio.Remover(lnq => lnq.Codigo == id).Result;
                return new Resposta<bool>(true);
            }
            catch (Exception e)
            {
                return new Resposta<bool>(false, $"Ocorreu um erro ao remover o curso com id {id}: " + e.Message);
            }
        }
    }
}
