﻿using MediatR;
using SGH.Dominio.Core;

namespace SGH.Dominio.Services.Implementacao.Cursos.Comandos.Remover
{
    public class RemoverCursoComando : IRequest<Resposta<bool>>
    {
        public int CursoId { get; set; }
    }
}
