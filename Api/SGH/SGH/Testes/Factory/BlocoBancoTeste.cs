using SGH.Api.Testes.Factory.Contratos;
using SGH.Dominio.Core.Model;
using SHG.Data.Contexto;
using System.Collections.Generic;

namespace SGH.Api.Testes.Factory
{
    public class BlocoBancoTeste : IBancoTeste<Bloco>
    {
        private readonly IContexto _contexto;

        public BlocoBancoTeste(IContexto contexto)
        {
            _contexto = contexto;
        }

        public void InicializarBanco()
        {
            var blocos = new List<Bloco> { 
                new Bloco
                {
                    Descricao = "Bloco 1"
                },
                new Bloco
                {
                    Descricao = "Bloco a remover"
                },
                new Bloco
                {
                    Descricao = "Bloco vinculado em salas"
                }
            };

            _contexto.Bloco.AddRange(blocos);
            _contexto.SaveChanges();
        }
    }
}
