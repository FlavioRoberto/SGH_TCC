﻿using MediatR;
using SGH.Dominio.Core;
using SGH.Dominio.Core.Model;

namespace SGH.Dominio.Implementacao.Cursos.Comandos.Criar
{
    public class CriarCursoComando : IRequest<Resposta<Curso>>
    {
        public string Descricao { get; set; }
    }
}
