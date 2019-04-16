using Data.Contexto;
using Dominio.Model;
using Dominio.ViewModel;
using Global;
using Microsoft.EntityFrameworkCore;

namespace Repositorio.Implementacao.CurriculoImplementacao
{
    public class CurriculoRepositorio : RepositorioBase<Dominio.Model.Curriculo>
    {
        private IContexto _contexto;

        public CurriculoRepositorio(MySqlContext contexto) : base(contexto)
        {
            _contexto = contexto;
        }

        public override Resposta<Paginacao<Dominio.Model.Curriculo>> ListarPorPaginacao(Paginacao<Dominio.Model.Curriculo> entidade)
        {
            throw new System.NotImplementedException();
        }

        protected override DbSet<Dominio.Model.Curriculo> GetDbSet()
        {
            return _contexto.Curriculo;
        }
    }
}
