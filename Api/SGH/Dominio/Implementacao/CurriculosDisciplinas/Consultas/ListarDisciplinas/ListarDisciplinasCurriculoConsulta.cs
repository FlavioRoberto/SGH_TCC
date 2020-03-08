using MediatR;
using SGH.Dominio.Core;
using SGH.Dominio.ViewModel;
using System.Collections.Generic;

namespace SGH.Dominio.Services.Implementacao.CurriculosDisciplinas.Consultas.ListarDisciplinas
{
    public class ListarDisciplinasCurriculoConsulta : IRequest<Resposta<List<CurriculoDisciplinaViewModel>>>
    {
        public int CodigoCurriculo { get; set; }
    }
}
