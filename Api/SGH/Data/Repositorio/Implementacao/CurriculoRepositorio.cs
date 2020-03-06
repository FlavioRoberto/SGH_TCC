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
    public class CurriculoRepositorio : ICurriculoRepositorio
    {
        private readonly IContexto _contexto;

        public CurriculoRepositorio(IContexto contexto) 
        {
            _contexto = contexto;
        }

        public async Task<Curriculo> Atualizar(Curriculo entidade)
        {
            try
            {
                await _contexto.IniciarTransacao();

                await RemoverDisciplinas(entidade.Codigo);

                foreach (var disciplina in entidade.Disciplinas)
                {
                    var preRequisitos = disciplina.CurriculoDisciplinaPreRequisito;

                    var disciplinaAdicionar = new CurriculoDisciplina
                    {
                        AulasSemanaisPratica = disciplina.AulasSemanaisPratica,
                        AulasSemanaisTeorica = disciplina.AulasSemanaisTeorica,
                        CodigoCurriculo = entidade.Codigo,
                        CodigoDisciplina = disciplina.CodigoDisciplina,
                        Periodo = disciplina.Periodo
                    };

                    _contexto.CurriculoDisciplina.Add(disciplinaAdicionar);
                    await _contexto.SaveChangesAsync();

                    foreach (var preRequisito in preRequisitos)
                    {
                        preRequisito.CodigoDisciplina = preRequisito.CodigoDisciplina;
                        preRequisito.CodigoCurriculoDisciplina = disciplinaAdicionar.Codigo;
                        _contexto.CurriculoDisciplinaPreRequisito.Add(preRequisito);
                        await _contexto.SaveChangesAsync();
                    };
                    
                }

                var curriculoAtualizar = await _contexto.Curriculo.FirstOrDefaultAsync(lnq => lnq.Codigo == entidade.Codigo);
                curriculoAtualizar.Ano = entidade.Ano;
                curriculoAtualizar.CodigoCurso = entidade.CodigoCurso;

                if (curriculoAtualizar.Disciplinas != null && curriculoAtualizar.Disciplinas.Any())
                    curriculoAtualizar.Disciplinas = curriculoAtualizar.Disciplinas.OrderBy(lnq => lnq.Periodo).ToList();

                await _contexto.SaveChangesAsync();

                _contexto.FecharTransacao();

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
                await _contexto.IniciarTransacao();
                                
                Curriculo curriculo = new Curriculo
                {
                    Codigo = entidade.Codigo,
                    Ano = entidade.Ano,
                    CodigoCurso = entidade.CodigoCurso,
                };

                _contexto.Curriculo.Add(curriculo);
                await _contexto.SaveChangesAsync();

                foreach (var curDis in entidade.Disciplinas)
                {
                    var curDisSalvar = new CurriculoDisciplina
                    {
                        CodigoCurriculo = curriculo.Codigo,
                        AulasSemanaisPratica = curDis.AulasSemanaisPratica,
                        AulasSemanaisTeorica = curDis.AulasSemanaisTeorica,
                        Codigo = curDis.Codigo,
                        Periodo = curDis.Periodo,
                        CodigoDisciplina = curDis.CodigoDisciplina,
                    };

                    _contexto.CurriculoDisciplina.Add(curDisSalvar);
                    await _contexto.SaveChangesAsync();

                    if (curDis.CurriculoDisciplinaPreRequisito != null && curDis.CurriculoDisciplinaPreRequisito.Any())
                    {
                        var preRequisitors = curDis.CurriculoDisciplinaPreRequisito.Select(curPre =>
                        {
                            return new CurriculoDisciplinaPreRequisito
                            {
                                CodigoCurriculoDisciplina = curDisSalvar.Codigo,
                                CodigoDisciplina = curPre.CodigoDisciplina
                            };
                        }).ToList();

                        _contexto.CurriculoDisciplinaPreRequisito.AddRange(preRequisitors);
                        await _contexto.SaveChangesAsync();
                    }
                }

                _contexto.FecharTransacao();

                var retorno = _contexto.Curriculo
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

        public async Task<List<CurriculoDisciplina>> ListarDisciplinas(int codigoCurriculo)
        {
            return await _contexto.CurriculoDisciplina.Include(lnq => lnq.Disciplina).Where(lnq => lnq.CodigoCurriculo == codigoCurriculo).AsNoTracking().ToListAsync();
        }

        public async Task<Paginacao<Curriculo>> ListarPorPaginacao(Paginacao<Curriculo> entidadePaginada)
        {
            var query = _contexto.Curriculo
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
            return await _contexto.CurriculoDisciplina.CountAsync(lnq => lnq.CodigoCurriculo == codigoCurriculo);
        }

        public async Task<CurriculoDisciplina> ConsultarCurriculoDisciplina(int codigoCurriculoDisciplina)
        {
            return await _contexto.CurriculoDisciplina.FirstOrDefaultAsync(lnq => lnq.Codigo == codigoCurriculoDisciplina);
        }

        public async Task<bool> Remover(int codigoCurriculo)
        {
            await _contexto.IniciarTransacao();
          
            await RemoverDisciplinas(codigoCurriculo);

            var curriculoRemover = await _contexto.Curriculo.AsNoTracking().FirstOrDefaultAsync(lnq => lnq.Codigo == codigoCurriculo);

            _contexto.Curriculo.Remove(curriculoRemover);

            await _contexto.SaveChangesAsync();

            _contexto.FecharTransacao();

            return true;
        }

        private async Task RemoverDisciplinas(int codigoCurriculo)
        {
            var disciplinasRemover = await _contexto.CurriculoDisciplina
                                            .Include(lnq => lnq.CurriculoDisciplinaPreRequisito)
                                            .AsNoTracking()
                                            .Where(lnq => lnq.CodigoCurriculo == codigoCurriculo)
                                            .ToListAsync();

            var disciplinaPreRequisitoRemover = new List<CurriculoDisciplinaPreRequisito>();

            disciplinasRemover.ForEach(disciplina => {
                if (disciplina.CurriculoDisciplinaPreRequisito != null)
                    disciplinaPreRequisitoRemover.AddRange(disciplina.CurriculoDisciplinaPreRequisito);
            });

            if (disciplinaPreRequisitoRemover != null)
            {
                _contexto.CurriculoDisciplinaPreRequisito.RemoveRange(disciplinaPreRequisitoRemover);
                await _contexto.SaveChangesAsync();
            }

            _contexto.CurriculoDisciplina.RemoveRange(disciplinasRemover);
            await _contexto.SaveChangesAsync();
        }

        public async Task<bool> Contem(Expression<Func<Curriculo, bool>> expressao)
        {
            return await _contexto.Curriculo.CountAsync(expressao) > 0;
        }

        public async Task<List<Curriculo>> ListarTodos()
        {
            return await _contexto.Curriculo.AsNoTracking().Include(lnq => lnq.Curso).ToListAsync();
        }
    }
}
