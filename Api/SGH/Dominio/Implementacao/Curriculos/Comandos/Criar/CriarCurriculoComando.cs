using System;
using MediatR;
using SGH.Dominio.Core;
using SGH.Dominio.Core.Model;

namespace SGH.Dominio.Services.Implementacao.Curriculos.Comandos.Criar
{
    public class CriarCurriculoComando : CurriculoComandoBase, IRequest<Resposta<Curriculo>>
    {
       
    }
}
