using Data.Contexto;
using Dominio.Model.CurriculoModel;
using Dominio.ViewModel;
using Global;
using Microsoft.EntityFrameworkCore;

namespace Repositorio.Implementacao.Curriculo
{
    public class CurriculoDisciplinaRepositorio : RepositorioBase<CurriculoDisciplina>
    {
        private IContexto _contexto;

        public CurriculoDisciplinaRepositorio(MySqlContext contexto) : base(contexto)
        {
            _contexto = contexto;
        }

        public override Resposta<Paginacao<CurriculoDisciplina>> ListarPorPaginacao(Paginacao<CurriculoDisciplina> entidade)
        {
            throw new System.NotImplementedException();
        }

        protected override DbSet<CurriculoDisciplina> GetDbSet()
        {
            return _contexto.CurriculoDisciplina;
        }
    }
}
