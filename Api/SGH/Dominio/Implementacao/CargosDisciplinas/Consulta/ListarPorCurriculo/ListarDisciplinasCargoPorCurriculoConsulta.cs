using MediatR;
using SGH.Dominio.Core;
using SGH.Dominio.Core.Enums;
using SGH.Dominio.Services.Implementacao.CargosDisciplinas.ViewModel;
using System.Collections.Generic;

namespace SGH.Dominio.Services.Implementacao.CargosDisciplinas.Consulta.ListarPorCurriculo
{
    public class ListarDisciplinaCargoPorCurriculoConsulta : IRequest<Resposta<ICollection<CargoDisciplinaListarPorCurriculoViewModel>>>
    {
        public int CodigoCurriculo { get; set; }
        public int Ano { get; set; }
        public int CodigoTurno { get; set; }
        public ESemestre Semestre { get; set; }
        public EPeriodo Periodo { get; set; }
    }
}
