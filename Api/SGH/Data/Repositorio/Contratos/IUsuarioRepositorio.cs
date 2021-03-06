﻿using SGH.Dominio.Core.Model;
using System.Threading.Tasks;

namespace SGH.Data.Repositorio.Contratos
{
    public interface IUsuarioRepositorio : IRepositorio<Usuario>, IRepositorioPaginacao<Usuario>
    {
        Task<Usuario> RetornarUsuarioPorLoginESenha(string login, string senha);
        Task<int> QuantidadeUsuarioAdm();
        Task<UsuarioPerfil> ConsultarPerfil(int codigoUsuarioLogado);
    }
}
