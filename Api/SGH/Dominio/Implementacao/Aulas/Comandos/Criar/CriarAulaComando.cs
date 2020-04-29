using MediatR;
using SGH.Dominio.Core;
using SGH.Dominio.Core.ObjetosValor;
using SGH.Dominio.Services.Implementacao.Aulas.ViewModels;

namespace SGH.Dominio.Services.Implementacao.Aulas.Comandos.Criar
{
    public class CriarAulaComando : IRequest<Resposta<AulaViewModel>>
    {
        public int CodigoHorario { get; set; }
        public int CodigoDisciplina { get; set; }
        public int CodigoSala { get; set; }
        public bool Laboratorio { get; set; }
        public bool Desdobramento { get; set; }
        public string DescricaoDesdobramento { get; set; }
        public Reserva Reserva { get; set; }
    }
}