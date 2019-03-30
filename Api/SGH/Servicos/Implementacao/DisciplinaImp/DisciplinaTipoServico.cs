using System;
using System.Collections.Generic;
using Dominio.Model.DisciplinaModel;
using Repositorio;
using Servico.Contratos.DisciplinaServico;

namespace Servico.Implementacao.DisciplinaImp
{
    public class DisciplinaTipoServico : IDisciplinaTipoServico
    {
        private readonly IRepositorio<DisciplinaTipo> _repositorio;

        public DisciplinaTipoServico(IRepositorio<DisciplinaTipo> repositorio)
        {
            _repositorio = repositorio;
        }
        
        public Resposta<DisciplinaTipo> Atualizar(DisciplinaTipo entidade)
        {
            try
            {
                var resultado = _repositorio.Atualizar(entidade).Result;
                return new Resposta<DisciplinaTipo>(resultado);
            }
            catch (Exception e)
            {
                return new Resposta<DisciplinaTipo>(entidade, $"Ocorreu um erro ao atualizar o tipo: {e.Message}");
            }
        }

        public Resposta<DisciplinaTipo> Criar(DisciplinaTipo entidade)
        {
            try
            {
                var resultado = _repositorio.Criar(entidade).Result;
                return new Resposta<DisciplinaTipo>(resultado);
            }
            catch (Exception e)
            {
                return new Resposta<DisciplinaTipo>(entidade, $"Ocorreu um erro ao criar o tipo: {e.Message}");
            }
        }

        public Resposta<DisciplinaTipo> ListarPeloId(long id)
        {
            try
            {
                var resultado = _repositorio.Listar(lnq => lnq.Codigo == id).Result;
                return new Resposta<DisciplinaTipo>(resultado);
            }
            catch (Exception e)
            {
                return new Resposta<DisciplinaTipo>(null, $"Ocorreu um erro ao listar o tipo com o código {id}: {e.Message}");
            }
        }

        public Resposta<List<DisciplinaTipo>> ListarTodos()
        {
            try
            {
                var resultado = _repositorio.ListarTodos().Result;
                return new Resposta<List<DisciplinaTipo>>(resultado);
            }
            catch (Exception e)
            {
                return new Resposta<List<DisciplinaTipo>>(null, $"Ocorreu um erro ao listar os tipos: {e.Message}");
            }
        }

        public Resposta<bool> Remover(long id)
        {
            try
            {
                var resultado = _repositorio.Remover(lnq => lnq.Codigo == id).Result;
                return new Resposta<bool>(resultado);
            }
            catch (Exception e)
            {
                return new Resposta<bool>(false, $"Ocorreu um erro ao remover o tipo com código {id}: {e.Message}");
            }
        }
    }
}
