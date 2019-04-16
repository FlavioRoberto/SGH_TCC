using System;
using System.Collections.Generic;
using Dominio.Model.DisciplinaModel;
using Dominio.ViewModel;
using Global;
using Repositorio;
using Servico.Contratos.DisciplinaServico;

namespace Servico.Implementacao.DisciplinaImp
{
    public class DisciplinaServico : IDisciplinaServico
    {
        private readonly IRepositorio<Disciplina> _repositorio;

        public DisciplinaServico(IRepositorio<Disciplina> repositorio)
        {
            _repositorio = repositorio;
        }

        public Resposta<Disciplina> Atualizar(Disciplina entidade)
        {
            try
            {
                var resultado = _repositorio.Atualizar(entidade).Result;
                return new Resposta<Disciplina>(resultado);
            }
            catch (Exception e)
            {
                return new Resposta<Disciplina>(entidade, $"Ocorreu um erro ao atualizar a disciplina: {e.Message}");
            }
        }

        public Resposta<Disciplina> Criar(Disciplina entidade)
        {
            try
            {
                var resultado = _repositorio.Criar(entidade).Result;
                return new Resposta<Disciplina>(resultado);
            }
            catch (Exception e)
            {
                return new Resposta<Disciplina>(entidade, $"Ocorreu um erro ao criar a disciplina: {e.Message}");
            }
        }

        public Resposta<Paginacao<Disciplina>> ListarComPaginacao(Paginacao<Disciplina> entidade)
        {
            try
            {
                var resultado = _repositorio.ListarPorPaginacao(entidade);
                if (resultado.TemErro())
                    return new Resposta<Paginacao<Disciplina>>(null, resultado.GetErros());

                return new Resposta<Paginacao<Disciplina>>(resultado.GetResultado());
            }
            catch (Exception e)
            {
                return new Resposta<Paginacao<Disciplina>>(null, $"Ocorreu um erro ao listar o turno: {e.Message}");
            }
        }

        public Resposta<Disciplina> ListarPeloId(long id)
        {
            try
            {
                var resultado = _repositorio.Listar(lnq => lnq.Codigo == id).Result;
                return new Resposta<Disciplina>(resultado);
            }
            catch (Exception e)
            {
                return new Resposta<Disciplina>(null, $"Ocorreu um erro ao listar a disciplina com o código {id}: {e.Message}");
            }
        }

        public Resposta<List<Disciplina>> ListarTodos()
        {
            try
            {
                var resultado = _repositorio.ListarTodos().Result;
                return new Resposta<List<Disciplina>>(resultado);
            }
            catch (Exception e)
            {
                return new Resposta<List<Disciplina>>(null, $"Ocorreu um erro ao listar as disciplinas: {e.Message}");
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
                return new Resposta<bool>(false, $"Ocorreu um erro ao remover a disciplina com o código {id}: {e.Message}");
            }
        }
    }
}
