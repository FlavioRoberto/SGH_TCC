using MediatR;
using SGH.Dominio.Core.Model;
using System.Collections.Generic;

namespace SGH.Dominio.Services.Implementacao.UsuarioPerfis.Consultas.ListarTodos
{
    public class ListarTodosPerfilConsulta : IRequest<List<UsuarioPerfil>>
    {
    }
}
