using System;
using System.Linq;
using System.Threading.Tasks;
using Data.Contexto;
using Dominio.Model.CurriculoModel;
using Dominio.ViewModel;
using Microsoft.EntityFrameworkCore;
using Repositorio.Helpers;

namespace Repositorio.Implementacao.Curriculo
{
    public class CurDisPreRequisitoRepositorio : RepositorioBase<CurriculoDisciplinaPreRequisito>
    {
        private IContexto _contexto;

        public CurDisPreRequisitoRepositorio(MySqlContext contexto) : base(contexto)
        {
            _contexto = contexto;
        }

        public override Paginacao<CurriculoDisciplinaPreRequisito> ListarPorPaginacao(Paginacao<CurriculoDisciplinaPreRequisito> entidadePaginada)
        {
            var query = _contexto.CurriculoDisciplinaPreRequisito
                        .Include(lnq=>lnq.CurriculoDisciplina)
                        .Include(lnq=>lnq.Disciplina)
                        .Skip(entidadePaginada.Posicao)
                        .AsNoTracking();

            if (entidadePaginada.Entidade == null)
                throw new Exception("Não é possível listar por paginação pois a entidade está nula");

            var entidade = entidadePaginada.Entidade;

            if (entidade.Codigo > 0)
                query.Where(lnq => lnq.Codigo == entidade.Codigo);

            if(entidade.CodigoCurriculoDisciplina > 0)
                query.Where(lnq => lnq.CodigoCurriculoDisciplina == entidade.CodigoCurriculoDisciplina);

            if(entidade.CodigoDisciplina > 0)
                query.Where(lnq => lnq.CodigoDisciplina == entidade.CodigoDisciplina);
            
           
            entidadePaginada.Entidade = query.Skip(entidadePaginada.Posicao).Take(1).FirstOrDefault();
            entidadePaginada.Total = query.Count();
            entidadePaginada.Posicao = query.ToList().IndexOf(entidadePaginada.Entidade);

            return PaginacaoMethod<CurriculoDisciplinaPreRequisito>.Paginar(entidadePaginada,query);

        }

        protected override DbSet<CurriculoDisciplinaPreRequisito> GetDbSet()
        {
            return _contexto.CurriculoDisciplinaPreRequisito;
        }
    }
}
