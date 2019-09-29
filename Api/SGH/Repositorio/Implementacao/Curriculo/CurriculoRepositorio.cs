using Data.Contexto;
using Dominio.Model;
using Dominio.Model.CurriculoModel;
using Dominio.ViewModel;
using Global;
using Microsoft.EntityFrameworkCore;
using Repositorio.Contratos;
using Repositorio.Helpers;
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

        public async Task<Curriculo> Atualizar(Curriculo entidade)
        {
            try
            {

                var disciplinasRemover = await _contexto.CurriculoDisciplina
                                             .AsNoTracking()
                                             .Where(lnq => lnq.CodigoCurriculo == entidade.Codigo)
                                             .ToListAsync();

                _contexto.CurriculoDisciplina.RemoveRange(disciplinasRemover);
                await _contexto.SaveChangesAsync();

                foreach (var disciplina in entidade.Disciplinas)
                {
                    var preRequisitos = disciplina.CurriculoDisciplinaPreRequisito;

                    var disciplinaAdicionar = new CurriculoDisciplina
                    {
                        AulasSemanaisPratica = disciplina.AulasSemanaisPratica,
                        AulasSemanaisTeorica = disciplina.AulasSemanaisTeorica,
                        CodigoCurriculo = entidade.Codigo,
                        CodigoDisciplina = disciplina.CodigoDisciplina,
                        Periodo = disciplina.Periodo,
                        Credito = disciplina.Credito
                    };

                    _contexto.CurriculoDisciplina.Add(disciplinaAdicionar);
                    _contexto.SaveChanges();

                    foreach (var preRequisito in preRequisitos)
                    {
                        preRequisito.CodigoDisciplina = preRequisito.CodigoDisciplina;
                        preRequisito.CodigoCurriculoDisciplina = disciplinaAdicionar.Codigo;
                        _contexto.CurriculoDisciplinaPreRequisito.Add(preRequisito);
                        _contexto.SaveChanges();
                    };

                }

                var curriculoAtualizar = await _contexto.Curriculo.FirstOrDefaultAsync(lnq => lnq.Codigo == entidade.Codigo);
                curriculoAtualizar.Ano = entidade.Ano;
                curriculoAtualizar.CodigoCurso = entidade.CodigoCurso;
                curriculoAtualizar.CodigoTurno = entidade.CodigoTurno;
                curriculoAtualizar.Turno = entidade.Turno;
                curriculoAtualizar.Disciplinas = curriculoAtualizar.Disciplinas.OrderBy(lnq => lnq.Periodo).ToList();

                await _contexto.SaveChangesAsync();

                return curriculoAtualizar;

            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<Curriculo> Criar(Curriculo entidade)
        {
            try
            {
                var curriculoExistente = _contexto.Curriculo.FirstOrDefault(lnq => lnq.Ano == entidade.Ano
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
                };

                _contexto.Curriculo.Add(curriculo);
                await _contexto.SaveChangesAsync();

                entidade.Disciplinas.ToList().ForEach(curDis =>
                {
                    var curDisSalvar = new CurriculoDisciplina
                    {
                        CodigoCurriculo = curriculo.Codigo,
                        AulasSemanaisPratica = curDis.AulasSemanaisPratica,
                        AulasSemanaisTeorica = curDis.AulasSemanaisTeorica,
                        Codigo = curDis.Codigo,
                        CodigoDisciplina = curDis.CodigoDisciplina,
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
                                .Include(lnq => lnq.Turno)
                                .Include(lnq => lnq.Curso)
                                .Include(lnq => lnq.Disciplinas)
                                .ThenInclude(ce => ce.Disciplina)
                                .Include(lnq => lnq.Disciplinas)
                                .ThenInclude(cp => cp.CurriculoDisciplinaPreRequisito)
                                .ThenInclude(dp => dp.Disciplina)
                                .FirstOrDefault(lnq => lnq.Codigo == curriculo.Codigo);

                retorno.Disciplinas = retorno.Disciplinas.OrderBy(lnq => lnq.Periodo).ToList();

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

        public async Task<Resposta<Paginacao<Curriculo>>> ListarPorPaginacao(Paginacao<Curriculo> entidadePaginada)
        {
            var query = _contexto.Curriculo
                                .Include(lnq => lnq.Turno)
                                .Include(lnq => lnq.Curso)
                                .Include(lnq => lnq.Disciplinas)
                                .ThenInclude(ce => ce.Disciplina)
                                .Include(lnq => lnq.Disciplinas)
                                .ThenInclude(cp => cp.CurriculoDisciplinaPreRequisito)
                                .ThenInclude(dp => dp.Disciplina)
                                .AsNoTracking();

            if (entidadePaginada.Entidade == null)
                entidadePaginada.Entidade = new Curriculo();

            var entidade = entidadePaginada.Entidade;

            if (entidade.Ano > 0)
                query = query.Where(lnq => lnq.Ano == entidade.Ano);

            if (entidade.Codigo > 0)
                query = query.Where(lnq => lnq.Codigo == entidade.Codigo);

            if (entidade.CodigoCurso > 0)
                query = query.Where(lnq => lnq.CodigoCurso == entidade.CodigoCurso);

            if (entidade.CodigoTurno > 0)
                query = query.Where(lnq => lnq.CodigoTurno == entidade.CodigoTurno);

            return await PaginacaoHelper<Curriculo>.Paginar(entidadePaginada, query);
        }

        public Task<List<Curriculo>> ListarTodos()
        {
            throw new NotImplementedException();
        }

        public async Task<bool> Remover(Expression<Func<Curriculo, bool>> query)
        {
            try
            {
                var item = await DbSet.FirstOrDefaultAsync(query);

                if (item != null)
                {
                    DbSet.Remove(item);
                    await _contexto.SaveChangesAsync();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
