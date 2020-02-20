using SGH.Api.Testes.Factory.Contratos;
using SGH.Dominio.Core.Model;
using SHG.Data.Contexto;
using System.Collections.Generic;

namespace SGH.Api.Testes.Factory
{
    public class CursoBancoTeste : ICursoBancoTeste
    {
        private readonly IContexto _contexto;

        public CursoBancoTeste(IContexto contexto)
        {
            _contexto = contexto;
        }

        public void InicializarBanco()
        {
            var cursos = new List<Curso>
            {
                new Curso
                {
                    Descricao = "Engenharia da computação"
                },
                new Curso
                {
                    Descricao = "Engenharia civil"
                },
                new Curso
                {
                    Descricao = "Engenharia de produção"
                }
            };

            _contexto.Curso.AddRange(cursos);
            _contexto.SaveChanges();
        }
    }
}
