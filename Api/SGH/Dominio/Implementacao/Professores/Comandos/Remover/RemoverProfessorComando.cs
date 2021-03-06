﻿using MediatR;
using SGH.Dominio.Core;

namespace SGH.Dominio.Services.Implementacao.Professores.Comandos.Remover
{
    public class RemoverProfessorComando :  IRequest<Resposta<bool>>
    {
        public int ProfessorId { get; set; }
    }
}
