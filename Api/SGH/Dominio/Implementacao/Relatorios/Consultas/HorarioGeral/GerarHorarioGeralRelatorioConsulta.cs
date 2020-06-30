using MediatR;
using SGH.Dominio.Core;
using SGH.Dominio.Core.Enums;

namespace SGH.Dominio.Services.Implementacao.Relatorios.Consultas.HorarioGeral
{
    public class GerarHorarioGeralRelatorioConsulta : IRequest<Resposta<string>>
    {
        public int CodigoCurso { get; set; }
        public int CodigoTurno { get; set; }
        public int Ano { get; set; }
        public ESemestre Semestre { get; set; }
    }
}
