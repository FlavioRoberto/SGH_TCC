﻿using MediatR;
using SGH.Dominio.Core;
using SGH.Dominio.Core.Model;

namespace SGH.Dominio.Implementacao.DIsciplinasTipoServico.Comandos.Criar
{
    public class CriarDisciplinaTipoComando : DisciplinaTipoComandoBase, IRequest<Resposta<DisciplinaTipo>>
    {
    }
}
