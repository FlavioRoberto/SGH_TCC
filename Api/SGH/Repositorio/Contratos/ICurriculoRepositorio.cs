using Dominio.Model;
using Dominio.Model.CurriculoModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Repositorio.Contratos
{
    public interface ICurriculoRepositorio : IRepositorio<Curriculo>
    {
        Task<List<CurriculoDisciplina>> ListarDisciplinas(int curriculoId);

    }
}
