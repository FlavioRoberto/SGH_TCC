using SGH.Api.Testes.Factory.Contratos;
using SGH.Dominio.Core.Model;
using SHG.Data.Contexto;
using System.Collections.Generic;

namespace SGH.Api.Testes.Factory
{
    public class DisciplinaBancoTeste : IBancoTeste<Disciplina>
    {
        private readonly IContexto _contexto;

        public DisciplinaBancoTeste(IContexto contexto)
        {
            _contexto = contexto;
        }

        public void InicializarBanco()
        {
            var disciplinas = new List<Disciplina>
            {
                new Disciplina
                {
                    Descricao = "Engenharia de software"
                },
                new Disciplina
                {
                    Descricao = "Programação orientada a objetos"
                },
                 new Disciplina
                {
                    Descricao = "Programação para dispositivos móveis"
                },
                 new Disciplina
                {
                    Descricao = "Concreto armado"
                },
                new Disciplina
                {
                    Descricao = "Cálculo I"
                }
            };

            _contexto.Disciplina.AddRange(disciplinas);
            _contexto.SaveChanges();
        }
    }
}
