using SGH.Api.Testes.Factory.Contratos;
using SGH.Dominio.Core.Model;
using SHG.Data.Contexto;
using System.Collections.Generic;

namespace SGH.Api.Testes.Factory
{
    public class ProfessorBancoTeste : IBancoTeste<Professor>
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
                new Professor //1
                {
                    Ativo = true,
                    Email = "professor1@gmail.com",
                    Matricula = "1629675",
                    Nome = "Professor 1",
                    Telefone = "37991456665"
                },
                new Professor //2
                {
                    Ativo = true,
                    Email = "professor2@gmail.com",
                    Matricula = "1829675",
                    Nome = "Professor 2",
                    Telefone = "37991456668"
                },
                new Professor //3
                {
                    Ativo = true,
                    Email="teste@teste.com",
                    Nome="Professor teste cargo",
                    Telefone="379885554"
                },
                new Professor //4
                {
                    Ativo = true,
                    Email="teste@teste.com",
                    Nome="Professor remover",
                    Telefone="379885554"
                },
                 new Professor //5
                {
                    Ativo = true,
                    Email="teste@teste.com",
                    Nome="Professor vinculado ao cargo remover",
                    Telefone="379885554"
                }
            };

            _contexto.Professor.AddRange(professores);
            _contexto.SaveChanges();
        }
    }
}
