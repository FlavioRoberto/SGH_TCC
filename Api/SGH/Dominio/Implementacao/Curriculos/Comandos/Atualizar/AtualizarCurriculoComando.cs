﻿using MediatR;
using SGH.Dominio.Core;
using SGH.Dominio.Core.Model;

namespace SGH.Dominio.Implementacao.Curriculos.Comandos.Atualizar
{
    public class AtualizarCurriculoComando : CurriculoComandoBase, IRequest<Resposta<Curriculo>>
    {    
    }
}
