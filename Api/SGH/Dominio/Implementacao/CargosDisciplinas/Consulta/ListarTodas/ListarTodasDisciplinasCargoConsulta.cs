using MediatR;
using SGH.Dominio.Core;
using SGH.Dominio.ViewModel;
using System.Collections.Generic;

namespace SGH.Dominio.Implementacao.CargosDisciplinas.Consulta.ListarTodas
{
    public class ListarTodasDisciplinasCargoConsulta : IRequest<Resposta<List<CargoDisciplinaViewModel>>>
    {
        public int CodigoCargo { get; set; }
    }
}
