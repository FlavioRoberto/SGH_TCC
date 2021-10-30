using MediatR;
using System;

namespace SGH.Dominio.Core.Events
{
    public abstract class Message : IRequest
    {
        public DateTime Inicio { get; private set; }

        public Message()
        {
            Inicio = DateTime.UtcNow;
        }
    }
}
