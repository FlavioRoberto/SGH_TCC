using MediatR;
using SGH.Dominio.Core;
using SGH.Dominio.Core.Model;
using System.Collections.Generic;

namespace SGH.Dominio.Implementacao.Disciplinas.Consultas.ListarPorDescricao
{
    public class ListarPorDescricaoDisciplinaConsulta : IRequest<Resposta<List<Disciplina>>>
    {
        public string Descricao { get; set; }
    }
}
