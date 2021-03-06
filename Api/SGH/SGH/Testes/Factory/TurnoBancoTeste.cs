﻿using SGH.Api.Testes.Factory.Contratos;
using SGH.Dominio.Core.Model;
using SHG.Data.Contexto;
using System.Collections.Generic;

namespace SGH.Api.Testes.Factory
{
    public class TurnoBancoTeste : IBancoTeste<Turno>
    {
        private readonly IContexto _contexto;

        public TurnoBancoTeste(IContexto contexto)
        {
            _contexto = contexto;
        }


        public void InicializarBanco()
        {
            var turnos = new List<Turno>
            {
                new Turno
                {
                    Descricao = "Matutino"
                },
                new Turno
                {
                    Descricao = "Vespertino"
                },
                new Turno
                {
                    Descricao = "Noturno"
                },
                new Turno
                {
                    Descricao = "Vinculado em horários"
                }
            };

            _contexto.Turno.AddRange(turnos);
            _contexto.SaveChanges();
        }
    }
}
