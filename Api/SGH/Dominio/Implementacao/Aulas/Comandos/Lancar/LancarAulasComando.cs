using MediatR;
using SGH.Dominio.Core;
using SGH.Dominio.Core.ObjetosValor;
using SGH.Dominio.Services.Implementacao.Aulas.Comandos.Base;
using System.Collections.Generic;

namespace SGH.Dominio.Services.Implementacao.Aulas.Comandos.Lancar
{
    public class LancarAulasComando : AulaComandoBase, IRequest<Resposta<List<string>>>
    {
        public List<Reserva> Reservas { get; private set; }

        public LancarAulasComando(List<Reserva> reservas)
        {
            Reservas = reservas;
        }
    }
}
