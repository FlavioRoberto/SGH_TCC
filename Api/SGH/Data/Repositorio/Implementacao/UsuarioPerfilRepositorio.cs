﻿using Microsoft.EntityFrameworkCore;
using SGH.Data.Repositorio.Contratos;
using SGH.Dominio.Core;
using SGH.Dominio.Core.Model;
using SHG.Data.Contexto;
using System.Threading.Tasks;

namespace SGH.Data.Repositorio.Implementacao
{
    public class UsuarioPerfilRepositorio : RepositorioBase<UsuarioPerfil>, IUsuarioPerfilRepositorio
    {
        public UsuarioPerfilRepositorio(IContexto contexto) : base(contexto)
        {
        }

        protected override DbSet<UsuarioPerfil> GetDbSet()
        {
            return _contexto.UsuarioPerfil;
        }
    }
}
