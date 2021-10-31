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
                new Aula //1
                {
                    Reserva = new Reserva("Quarta", "08:00"),
                    Desdobramento = false,
                    CodigoDisciplina = 1,
                    CodigoHorario = 1,
                    CodigoSala = 1,
                    Laboratorio = false
                },
                new Aula //2
                {
                    Reserva = new Reserva("Quarta", "09:00"),
                    Desdobramento = false,
                    CodigoDisciplina = 2,
                    CodigoHorario = 1,
                    CodigoSala = 1,
                    Laboratorio = false
                },
                new Aula //3
                {
                    Reserva = new Reserva("Quarta", "09:00"),
                    Desdobramento = false,
                    CodigoDisciplina = 3,
                    CodigoHorario = 3,
                    CodigoSala = 3,
                    Laboratorio = false
                },
                new Aula //4
                {
                    Reserva = new Reserva("Sexta", "11:00"),
                    Desdobramento = false,
                    CodigoDisciplina = 13,
                    CodigoHorario = 5,
                    CodigoSala = 2,
                    Laboratorio = false
                },
                new Aula //5 - Remover
                {
                    Reserva = new Reserva("Sexta", "18:00"),
                    Desdobramento = false,
                    CodigoDisciplina = 13,
                    CodigoHorario = 5,
                    CodigoSala = 2,
                    Laboratorio = false
                },
                 new Aula //6
                {
                    Reserva = new Reserva("Sexta", "18:00"),
                    Desdobramento = false,
                    CodigoDisciplina = 13,
                    CodigoHorario = 4,
                    CodigoSala = 2,
                    Laboratorio = false
                },
                 new Aula //7
                {
                    Reserva = new Reserva("Sabado", "07:00"),
                    Desdobramento = false,
                    CodigoDisciplina = 12,
                    CodigoHorario = 4,
                    CodigoSala = 2,
                    Laboratorio = false
                },
                 new Aula //8
                {
                    Reserva = new Reserva("Terça", "19:00"),
                    Desdobramento = false,
                    CodigoDisciplina = 12,
                    CodigoHorario = 2,
                    CodigoSala = 2,
                    Laboratorio = false
                },
                new Aula //9
                {
                    Reserva = new Reserva("Quinta", "19:00"),
                    Desdobramento = false,
                    CodigoDisciplina = 12,
                    CodigoHorario = 4,
                    CodigoSala = 2,
                    Laboratorio = false
                },
                new Aula //10
                {
                    Reserva = new Reserva("Quarta", "08:00"),
                    Desdobramento = false,
                    CodigoDisciplina = 1,
                    CodigoHorario = 1,
                    Laboratorio = false
                }
            });

            _contexto.SaveChanges();
        }
    }
}