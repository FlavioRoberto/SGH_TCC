using MediatR;
using SGH.Dominio.Core.Enums;
using SGH.Dominio.Services.ViewModel;
using System.Collections.Generic;

namespace SGH.Dominio.Services.Implementacao.Horarios.Consultas.Listar
{
    public class ListarHorarioAulaConsulta : IRequest<List<HorarioAulaViewModel>>
    {
        public int? CodigoCurriculo { get; set; }
        public EPeriodo? Periodo { get; set; }
        public ESemestre? Semestre { get; set; }
        public int? Ano { get; set; }
    }
}
