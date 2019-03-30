using Dominio.Model;
using Repositorio;
using Servico.Contratos;
using System;
using System.Collections.Generic;

namespace Servico.Implementacao
{
    public class TurnoServico : ITurnoServico
    {
        private readonly IRepositorio<Turno> _repositorio;

        public TurnoServico(IRepositorio<Turno> repositorio)
        {
            _repositorio = repositorio;
        }

        public Resposta<Turno> Atualizar(Turno entidade)
        {
            try
            {
                var resultado = _repositorio.Atualizar(entidade).Result;
                return new Resposta<Turno>(resultado);
            }
            catch (Exception e)
            {
                return new Resposta<Turno>(entidade, $"Ocorreu um erro ao atualizar o turno: {e.Message}");
            }
        }

        public Resposta<Turno> Criar(Turno entidade)
        {
            try
            {
                var resultado = _repositorio.Criar(entidade).Result;
                return new Resposta<Turno>(resultado);
            }
            catch (Exception e)
            {
                return new Resposta<Turno>(entidade, $"Ocorreu um erro ao criar o turno: {e.Message}");
            }
        }

        public Resposta<Turno> ListarPeloId(long id)
        {
            try
            {
                var resultado = _repositorio.Listar(lnq => lnq.Codigo == id).Result;
                return new Resposta<Turno>(resultado);
            }
            catch (Exception e)
            {
                return new Resposta<Turno>(null, $"Ocorreu um erro ao listar o turno com código {id}: {e.Message}");
            }
        }

        public Resposta<List<Turno>> ListarTodos()
        {
            try
            {
                var resultado = _repositorio.ListarTodos().Result;
                return new Resposta<List<Turno>>(resultado);
            }
            catch (Exception e)
            {
                return new Resposta<List<Turno>>(null, $"Ocorreu um erro ao listar os turnos: {e.Message}");
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
                return new Resposta<bool>(false, $"Ocorreu um erro ao remover o turno com código {id} : {e.Message} ");
            }
        }
    }
}
