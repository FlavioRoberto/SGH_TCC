using MediatR;
using SGH.Dominio.Core;
using SGH.Dominio.Core.Model;
using System.Collections.Generic;

namespace SGH.Dominio.Implementacao.Curriculos.Consultas.ListarDisciplinas
{
    public class ListarDisciplinasCurriculoConsulta : IRequest<Resposta<List<CurriculoDisciplina>>>
    {
        public int CodigoCurriculo { get; set; }
    }
}
