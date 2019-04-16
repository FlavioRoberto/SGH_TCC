using System;
using System.Collections.Generic;
using Dominio.Model.CurriculoModel;
using Dominio.ViewModel;
using Global;
using Repositorio;
using Servico.Contratos.CurriculoServico;

namespace Servico.Implementacao.CurriculoImp
{
    public class CurriculoDisciplinaPreRequisitoServico : ICurriculoDisciplinaPreRequisitoServico
    {

        private readonly IRepositorio<CurriculoDisciplinaPreRequisito> _repositorio;

        public CurriculoDisciplinaPreRequisitoServico(IRepositorio<CurriculoDisciplinaPreRequisito> repositorio)
        {
            _repositorio = repositorio;
        }

        public Resposta<CurriculoDisciplinaPreRequisito> Atualizar(CurriculoDisciplinaPreRequisito entidade)
        {
            try
            {
                var resultado = _repositorio.Atualizar(entidade).Result;
                return new Resposta<CurriculoDisciplinaPreRequisito>(resultado);
            }
            catch (Exception e)
            {
                return new Resposta<CurriculoDisciplinaPreRequisito>(entidade, $"Ocorreu um erro ao atualizar o pré-requisito: {e.Message}");
            }
        }

        public Resposta<CurriculoDisciplinaPreRequisito> Criar(CurriculoDisciplinaPreRequisito entidade)
        {
            try
            {
                var resultado = _repositorio.Criar(entidade).Result;
                return new Resposta<CurriculoDisciplinaPreRequisito>(resultado);
            }
            catch (Exception e)
            {
                return new Resposta<CurriculoDisciplinaPreRequisito>(entidade, $"Ocorreu um erro ao criar o pré-requisito: {e.Message}");
            }
        }

        public Resposta<Paginacao<CurriculoDisciplinaPreRequisito>> ListarComPaginacao(Paginacao<CurriculoDisciplinaPreRequisito> entidade)
        {
            try
            {
                var resultado =  _repositorio.ListarPorPaginacao(entidade);
                if (resultado.TemErro())
                    return new Resposta<Paginacao<CurriculoDisciplinaPreRequisito>>(null, resultado.GetErros());
                return new Resposta<Paginacao<CurriculoDisciplinaPreRequisito>>(resultado.GetResultado());
            }
            catch (Exception e)
            {
                return new Resposta<Paginacao<CurriculoDisciplinaPreRequisito>>(null, $"Ocorreu um erro ao listar o pré-requisito: {e.Message}");
            }
        }

        public Resposta<CurriculoDisciplinaPreRequisito> ListarPeloId(long id)
        {
            try
            {
                var resultado = _repositorio.Listar(lnq => lnq.Codigo == id).Result;
                return new Resposta<CurriculoDisciplinaPreRequisito>(resultado);
            }
            catch (Exception e)
            {
                return new Resposta<CurriculoDisciplinaPreRequisito>(null, $"Ocorreu um erro ao listar o pré-requisito com o código {id}: {e.Message}");
            }
        }

        public Resposta<List<CurriculoDisciplinaPreRequisito>> ListarTodos()
        {
            try
            {
                var resultado = _repositorio.ListarTodos().Result;
                return new Resposta<List<CurriculoDisciplinaPreRequisito>>(resultado);
            }
            catch (Exception e)
            {
                return new Resposta<List<CurriculoDisciplinaPreRequisito>>(null, $"Ocorreu um erro ao listar os pré-requisitos: {e.Message}");
            }
        }

        public Resposta<bool> Remover(long id)
        {
            try
            {
                var resultado = _repositorio.Remover(lnq => lnq.Codigo == id).Result;
                return new Resposta<bool>(resultado);
            }
            catch(Exception e)
            {
                return new Resposta<bool>(false, $"Ocorreu um erro ao remover o pré-requisito com o código {id}: {e.Message}");
            }
        }
    }
}
