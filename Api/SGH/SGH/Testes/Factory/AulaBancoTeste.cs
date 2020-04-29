using SGH.Api.Testes.Factory.Contratos;
using SGH.Dominio.Core.Model;
using SGH.Dominio.Core.ObjetosValor;
using SHG.Data.Contexto;
using System.Collections.Generic;

namespace SGH.Api.Testes.Factory
{
    public class AulaBancoTeste : IBancoTeste<Aula>
    {
        private readonly IContexto _contexto;

        public AulaBancoTeste(IContexto contexto)
        {
            _contexto = contexto;
        }

        public void InicializarBanco()
        {
            _contexto.Aula.AddRange(new List<Aula> {
                new Aula
                {
                    Reserva = new Reserva("Quarta", "08:00"),
                    Desdobramento = false,
                    CodigoDisciplina = 1,
                    CodigoHorario = 1,
                    CodigoSala = 1,
                    Laboratorio = false
                }
            });

            _contexto.SaveChanges();
        }
    }
}