using Dominio.Model;
using Repositorio;
using Servico.Contratos.CurriculoServico;
using System;
using System.Collections.Generic;

namespace Servico.Implementacao.CurriculoImp
{
    public class CurriculoServico : ICurriculoServico
    {
        private readonly IRepositorio<Curriculo> _repositorio;

        public CurriculoServico(IRepositorio<Curriculo> repositorio)
        {
            _repositorio = repositorio;
        }

        public Resposta<Curriculo> Atualizar(Curriculo entidade)
        {
            try
            {
                var resultado = _repositorio.Atualizar(entidade).Result;
                return new Resposta<Curriculo>(resultado);
            }
            catch (Exception e)
            {
                return new Resposta<Curriculo>(entidade, $"Ocorreu um erro ao atualizar o currículo: {e.Message}");
            }
        }

        public Resposta<Curriculo> Criar(Curriculo entidade)
        {
            try
            {
                var resultado = _repositorio.Criar(entidade).Result;
                return new Resposta<Curriculo>(resultado);
            }
            catch (Exception e)
            {
                return new Resposta<Curriculo>(entidade, $"Ocorreu um erro ao criar o currículo: {e.Message}");
            }
        }

        public Resposta<Curriculo> ListarPeloId(long id)
        {
            try
            {
                var resultado = _repositorio.Listar(lnq => lnq.Codigo == id).Result;
                return new Resposta<Curriculo>(resultado);
            }
            catch (Exception e)
            {
                return new Resposta<Curriculo>(null, $"Ocorreu um erro ao listar o currículo com código {id}: {e.Message}");
            }
        }

        public Resposta<List<Curriculo>> ListarTodos()
        {
            try
            {
                var resultado = _repositorio.ListarTodos().Result;
                return new Resposta<List<Curriculo>>(resultado);
            }
            catch (Exception e)
            {
                return new Resposta<List<Curriculo>>(null, $"Ocorreu um erro ao listar os currículos: {e.Message}");
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
                return new Resposta<bool>(false, $"Ocorreu um erro ao remover o currículo com código {id}: {e.Message}");
            }
        }
    }
}
