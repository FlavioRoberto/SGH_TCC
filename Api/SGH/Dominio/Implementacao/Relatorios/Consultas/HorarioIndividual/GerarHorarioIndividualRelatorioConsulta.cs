using MediatR;
using SGH.Dominio.Core;
using SGH.Dominio.Core.Enums;

namespace SGH.Dominio.Services.Implementacao.Relatorios.Consultas.HorarioIndividual
{
    public class GerarHorarioIndividualRelatorioConsulta : IRequest<Resposta<string>>
    {
        public int Ano { get; set; }
        public int CodigoProfessor { get; set; }
        public ESemestre Semestre { get; set; }
    }
}
