﻿using MediatR;
using SGH.Dominio.Core;
using SGH.Dominio.Core.Model;

namespace SGH.Dominio.Implementacao.DIsciplinasTipoServico.Comandos.Atualizar
{
    public class AtualizarDisciplinaTipoServicoComando : DisciplinaTipoComandoBase, IRequest<Resposta<DisciplinaTipo>>
    {
        public int Codigo { get; set; }
    }
}
