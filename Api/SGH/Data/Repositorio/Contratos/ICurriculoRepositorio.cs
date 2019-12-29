using SGH.Dominio.Core.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SGH.Data.Repositorio.Contratos
{
    public interface ICurriculoRepositorio : IRepositorio<Curriculo>
    {
        Task<List<CurriculoDisciplina>> ListarDisciplinas(int curriculoId);
        Task<int> RetornarQuantidadeDisciplinaCurriculo(int codigoCurriculo);
    }
}
