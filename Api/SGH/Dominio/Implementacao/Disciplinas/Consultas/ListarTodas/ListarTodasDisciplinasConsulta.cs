﻿using MediatR;
using SGH.Dominio.Core;
using SGH.Dominio.Core.Model;
using System.Collections.Generic;

namespace SGH.Dominio.Implementacao.Disciplinas.Consultas.ListarTodas
{
    public class ListarTodasDisciplinasConsulta : IRequest<Resposta<List<Disciplina>>>
    {
    }
}
