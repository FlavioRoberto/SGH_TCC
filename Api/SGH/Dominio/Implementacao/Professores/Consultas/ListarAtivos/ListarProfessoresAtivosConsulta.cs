using MediatR;
using SGH.Dominio.Core.Model;
using System.Collections.Generic;

namespace SGH.Dominio.Implementacao.Professores.Consultas.ListarAtivos
{
    public class ListarProfessoresAtivosConsulta : IRequest<ICollection<Professor>>
    {
    }
}
