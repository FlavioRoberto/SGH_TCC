using SGH.Api.Testes.Factory.Contratos;
using SGH.Dominio.Core.Extensions;
using SGH.Dominio.Core.Model;
using SHG.Data.Contexto;
using System.Collections.Generic;

namespace SGH.Api.Testes.Factory
{
    public class UsuarioBancoTeste : IUsuarioBancoTeste
    {
        private readonly IContexto _contexto;

        public UsuarioBancoTeste(IContexto contexto)
        {
            _contexto = contexto;
        }

        public void InicializarBanco()
        {
            var usuarios = new List<Usuario>
            {
                new Usuario {
                   Ativo = true,
                   Email = "admin@gmail.com",
                   Login = "admin",
                   Nome = "administrador",
                   PerfilCodigo = 1,
                   Senha = "admin".ToMD5(),
                   Telefone = "3732153995"
                },
                 new Usuario {
                   Ativo = false,
                   Email = "inativo@email.com",
                   Login = "inativo",
                   Nome = "Inativo",
                   PerfilCodigo = 1,
                   Senha = "inativo".ToMD5(),
                   Telefone = "3732153995"
                }
            };

            _contexto.Usuario.AddRange(usuarios);
            _contexto.SaveChanges();
        }
    }
}
