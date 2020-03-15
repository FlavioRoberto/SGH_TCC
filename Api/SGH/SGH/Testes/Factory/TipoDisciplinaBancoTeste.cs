using SGH.Api.Testes.Factory.Contratos;
using SGH.Dominio.Core.Model;
using SHG.Data.Contexto;
using System.Collections.Generic;

namespace SGH.Api.Testes.Factory
{
    public class TipoDisciplinaBancoTeste : IBancoTeste<DisciplinaTipo>
    {
        private readonly IContexto _contexto;

        public TipoDisciplinaBancoTeste(IContexto contexto)
        {
            _contexto = contexto;
        }

        public void InicializarBanco()
        {
            var tiposDisciplinas = new List<DisciplinaTipo>
            {
                new DisciplinaTipo
                {
                    Descricao = "Eletiva"
                },

                new DisciplinaTipo
                {
                    Descricao = "Optativa"
                }
            };

            _contexto.DisciplinaTipo.AddRange(tiposDisciplinas);
            _contexto.SaveChanges();
        }
    }
}
