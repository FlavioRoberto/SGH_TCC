﻿using MediatR;
using SGH.Dominio.Core;
using SGH.Dominio.Core.Model;

namespace SGH.Dominio.Services.Implementacao.Turnos.Comandos.Criar
{
    public class CriarTurnoComando : IRequest<Resposta<Turno>>
    {
        public string Descricao { get; set; }
    }
}
