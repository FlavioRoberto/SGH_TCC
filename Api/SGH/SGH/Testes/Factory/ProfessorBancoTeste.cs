using SGH.Api.Testes.Factory.Contratos;
using SGH.Dominio.Core.Model;
using SHG.Data.Contexto;
using System.Collections.Generic;

namespace SGH.Api.Testes.Factory
{
    public class ProfessorBancoTeste : IProfessorBancoTeste
    {
        private readonly IContexto _contexto;

        public ProfessorBancoTeste(IContexto contexto)
        {
            _contexto = contexto;
        }

        public void InicializarBanco()
        {
            var professores = new List<Professor>
            {
                new Professor
                {
                    Ativo = true,
                    Email = "teste@gmail.com",
                    Matricula = "1629675",
                    Nome = "Teste",
                    Telefone = "37991456665"
                }
            };

            _contexto.Professor.AddRange(professores);
            _contexto.SaveChanges();
        }
    }
}
