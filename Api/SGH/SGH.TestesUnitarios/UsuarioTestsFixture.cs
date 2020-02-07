

using Bogus;
using Bogus.DataSets;
using SGH.Dominio.Core.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace SGH.TestesDeUnidade
{
    [CollectionDefinition(nameof(UsuarioCollection))]
    public class UsuarioCollection: ICollectionFixture<UsuarioTestsFixture> { }

    public class UsuarioTestsFixture
    {
        public Usuario GerarUsuarioValido()
        {
            return GerarUsuariosValidos(1, true).FirstOrDefault();
        }

        public Usuario GerarUsuarioInvalido()
        {
            var genero = new Faker().PickRandom<Name.Gender>();

            var usuario = new Faker<Usuario>("pt_BR");

            usuario.RuleFor(lnq => lnq.Ativo, (f, u) => false);
            usuario.RuleFor(lnq => lnq.Codigo, (f, u) => f.Random.Number());
            usuario.RuleFor(lnq => lnq.Nome, (f, u) => "");
            usuario.RuleFor(lnq => lnq.Email, (f, u) => "");
            usuario.RuleFor(lnq => lnq.Telefone, (f, u) => f.Phone.PhoneNumber());
            usuario.RuleFor(lnq => lnq.PerfilCodigo, 0);
            usuario.RuleFor(lnq => lnq.Senha, (f, u) => f.Internet.Password());
            usuario.RuleFor(lnq => lnq.Login, (f, u) => "");

            return usuario;
        }

        public IEnumerable<Usuario> GerarUsuariosValidos(int quantidade, bool ativo)
        {
            var genero = new Faker().PickRandom<Name.Gender>();

            var usuarios = new Faker<Usuario>("pt_BR");

            usuarios.RuleFor(lnq => lnq.Ativo, (f, u) => ativo);
            usuarios.RuleFor(lnq => lnq.Codigo, (f, u) => f.Random.Number());
            usuarios.RuleFor(lnq => lnq.Nome, (f, u) => f.Name.FirstName(genero));
            usuarios.RuleFor(lnq => lnq.Email, (f, u) => f.Internet.Email(u.Nome));
            usuarios.RuleFor(lnq => lnq.Telefone, (f, u) => f.Phone.PhoneNumber());
            usuarios.RuleFor(lnq => lnq.PerfilCodigo, (f, u) => 1);
            usuarios.RuleFor(lnq => lnq.Senha, (f, u) => f.Internet.Password());
            usuarios.RuleFor(lnq => lnq.Login, (f, u) => f.Internet.UserName(u.Nome));

            return usuarios.Generate(quantidade);
        }

    }
}
