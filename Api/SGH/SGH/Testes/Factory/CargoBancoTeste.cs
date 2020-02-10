using SGH.Api.Testes.Factory.Contratos;
using SGH.Dominio.Core.Enums;
using SGH.Dominio.Core.Model;
using SHG.Data.Contexto;
using System;
using System.Collections.Generic;

namespace SGH.Api.Testes.Factory
{
    public class CargoBancoTeste : ICargoBancoTeste
    {
        private readonly IContexto _contexto;

        public CargoBancoTeste(IContexto contexto)
        {
            _contexto = contexto;
        }

        public void InicializarBanco()
        {

            var cargos = new List<Cargo>
            {
                new Cargo
                {
                    Ano = DateTime.Now.Year,
                    CodigoProfessor = 1,
                    Edital = 1,
                    Numero = 1,
                    Semestre = ESemestre.PRIMEIRO
                },
                new Cargo
                {
                    Ano = 2020,
                    CodigoProfessor = 2,
                    Edital = 2,
                    Numero = 2,
                    Semestre = ESemestre.SEGUNDO
                }
            };

            _contexto.Cargo.AddRange(cargos);
            _contexto.SaveChanges();

            var disciplinasCargo = new List<CargoDisciplina> {
                 new CargoDisciplina
                {
                   CodigoCargo = 1,
                   CodigoCurriculoDisciplina = 1
                },
                 new CargoDisciplina
                 {
                   CodigoCargo = 2,
                   CodigoCurriculoDisciplina = 1
                 },
                  new CargoDisciplina
                 {
                   CodigoCargo = 2,
                   CodigoCurriculoDisciplina = 2
                 }
            };

            _contexto.CargoDisciplina.AddRange(disciplinasCargo);
            _contexto.SaveChanges();
        }
    }
}
