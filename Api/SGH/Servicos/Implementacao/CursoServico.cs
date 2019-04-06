using System;
using System.Collections.Generic;
using Dominio.Model;
using Dominio.ViewModel;
using Repositorio;
using Servico.Contratos;

namespace Servico.Implementacao
{
    public class CursoServico : ICursoServico
    {
        private readonly IRepositorio<Curso> _repositorio;

        public CursoServico(IRepositorio<Curso> repositorio)
        {
            _repositorio = repositorio;
        }

        public Resposta<Curso> Atualizar(Curso entidade)
        {
            try
            {
                var resultado = _repositorio.Atualizar(entidade).Result;
                return new Resposta<Curso>(resultado);
            }
            catch (Exception e)
            {
                return new Resposta<Curso>(entidade, "Ocorreu um erro ao atualizar o curso: " + e.Message);
            }
        }

        public Resposta<Paginacao<Curso>> ListarComPaginacao(Paginacao<Curso> entidade)
        {
            try
            {
                var resultado = _repositorio.ListarPorPaginacao(entidade);
                return new Resposta<Paginacao<Curso>>(resultado);
            }
            catch (Exception e)
            {
                return new Resposta<Paginacao<Curso>>(null, $"Ocorreu um erro ao listar o curso: {e.Message}");
            }
        }

        public Resposta<Curso> Criar(Curso entidade)
        {
            try
            {
                var resultado = _repositorio.Criar(entidade).Result;
                return new Resposta<Curso>(resultado);
            }
            catch (Exception e)
            {
                return new Resposta<Curso>(entidade,"Ocorreu um erro ao criar o curso: "+e.Message);
            }
        }

        public Resposta<Curso> ListarPeloId(long id)
        {
            try
            {
                var resultado = _repositorio.Listar(lnq => lnq.Codigo == id).Result;
                return new Resposta<Curso>(resultado);
            }
            catch (Exception e)
            {
                return new Resposta<Curso>(null, $"Ocorreu um erro ao listar o curso pelo id: {id}: " + e.Message);
            }
        }

        public Resposta<List<Curso>> ListarTodos()
        {
            try
            {
                var resultado = _repositorio.ListarTodos().Result;
                return new Resposta<List<Curso>>(resultado);
            }
            catch (Exception e)
            {
                return new Resposta<List<Curso>>(null, "Ocorreu um erro ao listar os cursos: " + e.Message);
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
