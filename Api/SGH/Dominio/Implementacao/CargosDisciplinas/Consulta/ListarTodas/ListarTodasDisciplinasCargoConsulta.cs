using MediatR;
using SGH.Dominio.ViewModel;
using System.Collections.Generic;

namespace SGH.Dominio.Implementacao.CargosDisciplinas.Consulta.ListarTodas
{
    public class ListarTodasDisciplinasCargoConsulta : IRequest<List<CargoDisciplinaViewModel>>
    {
        public int CodigoCargo { get; set; }
    }
}
