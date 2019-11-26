using Data.Contexto;
using Dominio.Model.DisciplinaModel;
using Dominio.ViewModel;
using Global;
using Microsoft.EntityFrameworkCore;
using Repositorio.Helpers;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Repositorio.Implementacao.Disciplina
{
    public class DisciplinaRepositorio : RepositorioBase<Dominio.Model.DisciplinaModel.Disciplina>
    {

        public DisciplinaRepositorio(MySqlContext contexto) : base(contexto)
        {
        }

        public override async Task<Resposta<Paginacao<Dominio.Model.DisciplinaModel.Disciplina>>> ListarPorPaginacao(Paginacao<Dominio.Model.DisciplinaModel.Disciplina> entidadePaginada)
        {
            var query = GetDbSet().AsNoTracking();

            if (entidadePaginada.Entidade == null)
                entidadePaginada.Entidade = new List<Dominio.Model.DisciplinaModel.Disciplina>();

            var entidade = entidadePaginada.Entidade.FirstOrDefault();

            if (entidade.Codigo > 0)
                query = query.Where(lnq => lnq.Codigo == entidade.Codigo);

            if (entidade.CodigoTipo > 0)
                query = query.Where(lnq => lnq.CodigoTipo == entidade.CodigoTipo);

            if (!string.IsNullOrEmpty(entidade.Descricao))
                query = query.Where(lnq => lnq.Descricao.Contains(entidade.Descricao));


            return await PaginacaoHelper<Dominio.Model.DisciplinaModel.Disciplina>.Paginar(entidadePaginada, query);
        }

        protected override Microsoft.EntityFrameworkCore.DbSet<Dominio.Model.DisciplinaModel.Disciplina> GetDbSet()
        {
            return _contexto.Disciplina;
        }
    }
}
