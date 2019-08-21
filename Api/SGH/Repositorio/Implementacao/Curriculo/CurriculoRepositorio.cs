using Data.Contexto;
using Dominio.Model;
using Dominio.Model.CurriculoModel;
using Dominio.ViewModel;
using Global;
using Microsoft.EntityFrameworkCore;
using Repositorio.Contratos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Repositorio.Implementacao.CurriculoImplementacao
{
    public class CurriculoRepositorio : ICurriculoRepositorio
    {

        IContexto _contexto;

        public CurriculoRepositorio(MySqlContext contexto)
        {
            _contexto = contexto;
        }

        public DbSet<Curriculo> DbSet => _contexto.Curriculo;

        public Task<Curriculo> Atualizar(Curriculo entidade)
        {
            throw new NotImplementedException();
        }

        public async Task<Curriculo> Criar(Curriculo entidade)
        {
            try
            {
                var curriculoExistente = _contexto.Curriculo.FirstOrDefault(lnq => lnq.Ano == entidade.Ano
                                                                && lnq.Periodo == entidade.Periodo
                                                                && lnq.CodigoCurso == entidade.CodigoCurso
                                                                && lnq.CodigoTurno == entidade.CodigoTurno) != null;

                if (curriculoExistente)
                    throw new Exception("Já existe um currículo cadastrado com os dados informados!");

                Curriculo curriculo = new Curriculo
                {
                    Codigo = entidade.Codigo,
                    Ano = entidade.Ano,
                    CodigoCurso = entidade.CodigoCurso,
                    CodigoTurno = entidade.CodigoTurno,
                    Periodo = entidade.Periodo
                };
         
                _contexto.Curriculo.Add(curriculo);
                await _contexto.SaveChangesAsync();

                entidade.Disciplinas.ForEach(curDis =>
                {
                    var curDisSalvar = new CurriculoDisciplina
                    {
                        CodigoCurriculo = curriculo.Codigo,
                        CargaHorariaSemanalPratica = curDis.CargaHorariaSemanalPratica,
                        CargaHorariaSemanalTeorica = curDis.CargaHorariaSemanalTeorica,
                        Codigo = curDis.Codigo,
                        CodigoDisciplina = curDis.CodigoDisciplina,
                        Credito = curDis.Credito,
                        HoraAulaTotal = curDis.HoraAulaTotal,
                        HoraTotal = curDis.HoraTotal
                    };

                    _contexto.CurriculoDisciplina.Add(curDisSalvar);
                    _contexto.SaveChanges();

                    var preRequisitors = curDis.CurriculoDisciplinaPreRequisito.Select(curPre =>
                    {
                        return new CurriculoDisciplinaPreRequisito
                        {
                            CodigoCurriculoDisciplina = curDisSalvar.Codigo,
                            CodigoDisciplina = curPre.CodigoDisciplina
                        };
                    }).ToList();

                    _contexto.CurriculoDisciplinaPreRequisito.AddRange(preRequisitors);
                    _contexto.SaveChanges();

                });

                var retorno = DbSet
                                .Include(lnq=>lnq.Turno)
                                .Include(lnq=>lnq.Curso)
                                .Include(lnq=>lnq.Disciplinas)
                                .FirstOrDefault(lnq => lnq.Codigo == curriculo.Codigo);

                return retorno;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public Task<Curriculo> Listar(Expression<Func<Curriculo, bool>> query)
        {
            throw new NotImplementedException();
        }

        public Task<List<Curriculo>> ListarPor(Expression<Func<Curriculo, bool>> query)
        {
            throw new NotImplementedException();
        }

        public Task<Resposta<Paginacao<Curriculo>>> ListarPorPaginacao(Paginacao<Curriculo> entidade)
        {
            throw new NotImplementedException();
        }

        public Task<List<Curriculo>> ListarTodos()
        {
            throw new NotImplementedException();
        }

        public Task<bool> Remover(Expression<Func<Curriculo, bool>> query)
        {
            throw new NotImplementedException();
        }
    }
}
