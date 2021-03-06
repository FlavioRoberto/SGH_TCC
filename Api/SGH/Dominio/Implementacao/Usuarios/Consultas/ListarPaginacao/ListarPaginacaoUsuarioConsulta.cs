﻿using MediatR;
using SGH.Dominio.Core.Model;

namespace SGH.Dominio.Services.Implementacao.Usuarios.Consultas.ListarPaginacao
{
    public class ListarPaginacaoUsuarioConsulta : IRequest<Paginacao<Usuario>>
    {
        public Paginacao<Usuario> UsuarioPaginado { get; set; }
    }
}
