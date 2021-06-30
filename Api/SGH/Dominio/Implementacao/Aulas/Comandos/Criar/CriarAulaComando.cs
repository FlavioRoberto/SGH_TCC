using MediatR;
using SGH.Dominio.Core;
using SGH.Dominio.Core.ObjetosValor;
using SGH.Dominio.Services.Implementacao.Aulas.Comandos.Base;
using SGH.Dominio.Services.Implementacao.Aulas.ViewModels;

namespace SGH.Dominio.Services.Implementacao.Aulas.Comandos.Criar
{
    public class CriarAulaComando : AulaComandoBase, IRequest<Resposta<AulaViewModel>>
    {
        public bool Desdobramento { get; set; }
        public string DescricaoDesdobramento { get; set; }
        public Reserva Reserva { get; set; }
    }
}