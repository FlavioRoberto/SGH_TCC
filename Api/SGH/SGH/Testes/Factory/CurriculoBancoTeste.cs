using SGH.Api.Testes.Factory.Contratos;
using SGH.Dominio.Core.Model;
using SHG.Data.Contexto;
using System;
using System.Collections.Generic;

namespace SGH.Api.Testes.Factory
{
    public class CurriculoBancoTeste : ICurriculoBancoTeste
    {
        private readonly IContexto _contexto;

        public CurriculoBancoTeste(IContexto contexto)
        {
            _contexto = contexto;
        }

        public void InicializarBanco()
        {
            var curriculos = new List<Curriculo>
            {
                new Curriculo
                {
                    Ano = DateTime.Now.Year,
                    CodigoCurso = 1
                }
            };

            _contexto.Curriculo.AddRange(curriculos);
            _contexto.SaveChanges();

        }
    }
}
