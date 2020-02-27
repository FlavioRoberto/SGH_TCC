﻿using MediatR;
using SGH.Dominio.Core.Model;

namespace SGH.Dominio.Services.Implementacao.Turnos.Consultas.ListarPaginacao
{
    public class ListarPaginacaoTurnoConsulta : IRequest<Paginacao<Turno>>
    {
        public Paginacao<Turno> TurnoPaginado { get; set; }
    }
}
