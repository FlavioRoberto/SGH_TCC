using SGH.Dominio.Core.Model;

namespace SGH.Dominio.Core.Contratos
{
    public interface ICursoRepositorio : IRepositorio<Curso>, IRepositorioPaginacao<Curso>
    {
    }
}
