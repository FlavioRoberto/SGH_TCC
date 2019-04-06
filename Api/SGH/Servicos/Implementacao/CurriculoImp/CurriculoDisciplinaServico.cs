using System;
using System.Collections.Generic;
using Dominio.Model.CurriculoModel;
using Dominio.ViewModel;
using Repositorio;
using Servico.Contratos.CurriculoServico;

namespace Servico.Implementacao.CurriculoImp
{
    public class CurriculoDisciplinaServico : ICurriculoDisciplinaServico
    {
        private readonly IRepositorio<CurriculoDisciplina> _repositorio;

        public CurriculoDisciplinaServico(IRepositorio<CurriculoDisciplina> repositorio)
        {
            _repositorio = repositorio;
        }

        public Resposta<CurriculoDisciplina> Atualizar(CurriculoDisciplina entidade)
        {
            try
            {
                var resultado = _repositorio.Atualizar(entidade).Result;
                return new Resposta<CurriculoDisciplina>(resultado);
            }
            catch(Exception e)
            {
                return new Resposta<CurriculoDisciplina>(entidade, $"Ocorreu um erro ao atualizar a disciplina: {e.Message}");
            }
        }


        public Resposta<CurriculoDisciplina> Criar(CurriculoDisciplina entidade)
        {
            try
            {
                var resultado = _repositorio.Criar(entidade).Result; 
                return new Resposta<CurriculoDisciplina>(resultado);
            }
            catch(Exception e)
            {
                return new Resposta<CurriculoDisciplina>(entidade, $"Ocorreu um erro ao adicionar a disciplina: {e.Message}");
            }
        }

        public Resposta<Paginacao<CurriculoDisciplina>> ListarComPaginacao(Paginacao<CurriculoDisciplina> entidade)
        {
            try
            {
                var resultado = _repositorio.ListarPorPaginacao(entidade);
                return new Resposta<Paginacao<CurriculoDisciplina>>(resultado);
            }
            catch (Exception e)
            {
                return new Resposta<Paginacao<CurriculoDisciplina>>(null, $"Ocorreu um erro ao listar a disciplina: {e.Message}");
            }
        }

        public Resposta<CurriculoDisciplina> ListarPeloId(long id)
        {
            try
            {
                var resultado = _repositorio.Listar(lnq => lnq.Codigo == id).Result;
                return new Resposta<CurriculoDisciplina>(resultado);
            }
            catch(Exception e)
            {
                return new Resposta<CurriculoDisciplina>(null, $"Ocorreu um erro ao listar a disciplina com código {id}: {e.Message}");
            }
        }

        public Resposta<List<CurriculoDisciplina>> ListarTodos()
        {
            try
            {
                var resultado = _repositorio.ListarTodos().Result;
                return new Resposta<List<CurriculoDisciplina>>(resultado);
            }
            catch (Exception e)
            {
                return new Resposta<List<CurriculoDisciplina>>(null, $"Ocorreu um erro ao listar as disciplinas: {e.Message}");
            }
        }

        public Resposta<bool> Remover(long id)
        {
            try
            {
                var resultado = _repositorio.Remover(lnq=>lnq.Codigo == id).Result;
                return new Resposta<bool>(resultado);
            }
            catch(Exception e)
            {
                return new Resposta<bool>(false, $"Ocorreu um erro ao remover a disciplina com código {id}: {e.Message}");
            }
        }
    }
}
