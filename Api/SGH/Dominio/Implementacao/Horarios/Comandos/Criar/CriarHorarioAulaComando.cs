using MediatR;
using SGH.Dominio.Core;
using SGH.Dominio.Core.Enums;
using SGH.Dominio.Services.ViewModel;

namespace SGH.Dominio.Services.Implementacao.Horarios.Comandos.Criar
{
    public class CriarHorarioAulaComando: IRequest<Resposta<HorarioAulaViewModel>>
    {
        public int Ano { get; set; }
        public ESemestre Semestre { get; set; }
        public EPeriodo Periodo { get; set; }
        public int CodigoTurno { get; set; }
        public int CodigoCurriculo { get; set; }
    }
}
