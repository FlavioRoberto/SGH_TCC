using SGH.Api.Testes.Factory.Contratos;
using SGH.Dominio.Core.Model;
using SHG.Data.Contexto;
using System.Collections.Generic;

namespace SGH.Api.Testes.Factory
{
    public class UsuarioPerfilBancoTeste : IBancoTeste<UsuarioPerfil>
    {
        private IContexto _contexto;

        public UsuarioPerfilBancoTeste(IContexto contexto)
        {
            _contexto = contexto;
        }

        public void InicializarBanco()
        {
            var perfis = new List<UsuarioPerfil>
            {
              new UsuarioPerfil
              {
                  Administrador = true,
                  Descricao = "Administrador"
              }
            };

            _contexto.UsuarioPerfil.AddRange(perfis);
            _contexto.SaveChanges(); 
        }
    }
}
