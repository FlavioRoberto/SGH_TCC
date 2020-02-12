using SGH.Api.Testes.Factory.Contratos;
using SGH.Dominio.Core.Model;
using SHG.Data.Contexto;
using System.Collections.Generic;

namespace SGH.Api.Testes.Factory
{
    public class DisciplinaBancoTeste : IDisciplinaBancoTeste
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
                    CodigoTipo = 1,
                    Descricao = "Engenharia de software"
                },
                new Disciplina
                {
                    CodigoTipo = 1,
                    Descricao = "Programação orientada a objetos"
                },
                 new Disciplina
                {
                    CodigoTipo = 2,
                    Descricao = "Programação para dispositivos móveis"
                }
            };

            _contexto.Disciplina.AddRange(disciplinas);
            _contexto.SaveChanges();
        }
    }
}
