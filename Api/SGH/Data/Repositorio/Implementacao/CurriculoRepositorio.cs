using Microsoft.EntityFrameworkCore;
using SGH.Data.Repositorio.Contratos;
using SGH.Data.Repositorio.Helpers;
using SGH.Dominio.Core.Model;
using SHG.Data.Contexto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SGH.Data.Repositorio.Implementacao
{
    public class CurriculoRepositorio : RepositorioBase<Curriculo>, ICurriculoRepositorio
    {

        public CurriculoRepositorio(IContexto contexto) : base(contexto)
        {
        }

        public override async Task<Curriculo> Atualizar(Curriculo entidade)
        {
            try
            {

                var disciplinasRemover = await GetDbSet<CurriculoDisciplina>()
                                                .AsNoTracking()
                                                .Where(lnq => lnq.CodigoCurriculo == entidade.Codigo)
                                                .ToListAsync();

                GetDbSet<CurriculoDisciplina>().RemoveRange(disciplinasRemover);
                await SaveChangesAsync();

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

                    GetDbSet<CurriculoDisciplina>().Add(disciplinaAdicionar);
                    await SaveChangesAsync();

                    foreach (var preRequisito in preRequisitos)
                    {
                        preRequisito.CodigoDisciplina = preRequisito.CodigoDisciplina;
                        preRequisito.CodigoCurriculoDisciplina = disciplinaAdicionar.Codigo;
                        GetDbSet<CurriculoDisciplinaPreRequisito>().Add(preRequisito);
                        await SaveChangesAsync();
                    };

                }

                var curriculoAtualizar = await GetDbSet<Curriculo>().FirstOrDefaultAsync(lnq => lnq.Codigo == entidade.Codigo);
                curriculoAtualizar.Ano = entidade.Ano;
                curriculoAtualizar.CodigoCurso = entidade.CodigoCurso;
                curriculoAtualizar.Disciplinas = curriculoAtualizar.Disciplinas.OrderBy(lnq => lnq.Periodo).ToList();

                await SaveChangesAsync();

                return curriculoAtualizar;

            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public override async Task<Curriculo> Criar(Curriculo entidade)
        {
            try
            {
                Curriculo curriculo = new Curriculo
                {
                    Codigo = entidade.Codigo,
                    Ano = entidade.Ano,
                    CodigoCurso = entidade.CodigoCurso,
                };

                GetDbSet<Curriculo>().Add(curriculo);
                await SaveChangesAsync();

                foreach (var curDis in entidade.Disciplinas)
                {
                    var curDisSalvar = new CurriculoDisciplina
                    {
                        CodigoCurriculo = curriculo.Codigo,
                        AulasSemanaisPratica = curDis.AulasSemanaisPratica,
                        AulasSemanaisTeorica = curDis.AulasSemanaisTeorica,
                        Codigo = curDis.Codigo,
                        Periodo = curDis.Periodo,
                        Credito = curDis.Credito,
                        CodigoDisciplina = curDis.CodigoDisciplina,
                    };

                    GetDbSet<CurriculoDisciplina>().Add(curDisSalvar);
                    await SaveChangesAsync();

                    var preRequisitors = curDis.CurriculoDisciplinaPreRequisito.Select(curPre =>
                    {
                        return new CurriculoDisciplinaPreRequisito
                        {
                            CodigoCurriculoDisciplina = curDisSalvar.Codigo,
                            CodigoDisciplina = curPre.CodigoDisciplina
                        };
                    }).ToList();

                    GetDbSet<CurriculoDisciplinaPreRequisito>().AddRange(preRequisitors);
                    await SaveChangesAsync();
                }

                var retorno = GetDbSet<Curriculo>()
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

        public override async Task<Curriculo> Consultar(Expression<Func<Curriculo, bool>> query)
        {
            return await GetDbSet<Curriculo>().Include(lnq => lnq.Curso).Where(query).AsNoTracking().FirstOrDefaultAsync();
        }

        public async Task<List<CurriculoDisciplina>> ListarDisciplinas(int codigoCurriculo)
        {
            return await GetDbSet<CurriculoDisciplina>().Include(lnq => lnq.Disciplina).Where(lnq => lnq.CodigoCurriculo == codigoCurriculo).AsNoTracking().ToListAsync();
        }

        public override async Task<List<Curriculo>> Listar(Expression<Func<Curriculo, bool>> query)
        {
            return await GetDbSet<Curriculo>().Include(lnq => lnq.Curso).Where(query).AsNoTracking().ToListAsync();
        }

        public async Task<Paginacao<Curriculo>> ListarPorPaginacao(Paginacao<Curriculo> entidadePaginada)
        {
            var query = GetDbSet<Curriculo>()
                                .Include(lnq => lnq.Curso)
                                .Include(lnq => lnq.Disciplinas)
                                .ThenInclude(ce => ce.Disciplina)
                                .Include(lnq => lnq.Disciplinas)
                                .ThenInclude(cp => cp.CurriculoDisciplinaPreRequisito)
                                .ThenInclude(dp => dp.Disciplina)
                                .AsNoTracking();

            if (entidadePaginada.Entidade == null)
                entidadePaginada.Entidade = new List<Curriculo>();

            var entidade = entidadePaginada.Entidade.FirstOrDefault();

            if (entidade.Ano > 0)
                query = query.Where(lnq => lnq.Ano == entidade.Ano);

            if (entidade.Codigo > 0)
                query = query.Where(lnq => lnq.Codigo == entidade.Codigo);

            if (entidade.CodigoCurso > 0)
                query = query.Where(lnq => lnq.CodigoCurso == entidade.CodigoCurso);

            return await PaginacaoHelper<Curriculo>.Paginar(entidadePaginada, query);
        }

        public async Task<int> RetornarQuantidadeDisciplinaCurriculo(int codigoCurriculo)
        {
            return await GetDbSet<CurriculoDisciplina>().CountAsync(lnq => lnq.CodigoCurriculo == codigoCurriculo);
        }

        public async Task<CurriculoDisciplina> ConsultarCurriculoDisciplina(int codigoCurriculoDisciplina)
        {
            return await GetDbSet<CurriculoDisciplina>().FirstOrDefaultAsync(lnq => lnq.Codigo == codigoCurriculoDisciplina);
        }
    }
}
