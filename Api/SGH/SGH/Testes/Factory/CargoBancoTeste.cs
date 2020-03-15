using SGH.Api.Testes.Factory.Contratos;
using SGH.Dominio.Core.Enums;
using SGH.Dominio.Core.Model;
using SHG.Data.Contexto;
using System;
using System.Collections.Generic;

namespace SGH.Api.Testes.Factory
{
    public class CargoBancoTeste : IBancoTeste<Cargo>
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
                },
                new Cargo //cargo a editar codigo 3
                {
                    Ano = 2020,
                    CodigoProfessor = 20,
                    Edital = 2,
                    Numero = 2,
                    Semestre = ESemestre.SEGUNDO
                },
                new Cargo //cargo para consulta codigo 4
                {
                    Ano = 2020,
                    CodigoProfessor = 2,
                    Edital = 13,
                    Semestre = ESemestre.PRIMEIRO
                }
            };

            _contexto.Cargo.AddRange(cargos);
            _contexto.SaveChanges();

            var disciplinasCargo = new List<CargoDisciplina> {
                 new CargoDisciplina
                 {
                    CodigoCargo = 1,
                    CodigoCurriculoDisciplina = 1,
                    CodigoTurno = 1
                 },
                 new CargoDisciplina
                 {
                    CodigoCargo = 2,
                    CodigoCurriculoDisciplina = 1,
                    CodigoTurno = 2
                 },
                  new CargoDisciplina
                 {
                    CodigoCargo = 2,
                    CodigoCurriculoDisciplina = 2,
                    CodigoTurno = 3
                 },
                  new CargoDisciplina
                 {
                    CodigoCargo = 3,
                    CodigoCurriculoDisciplina = 1,
                    CodigoTurno = 1
                 },
                  new CargoDisciplina
                 {
                    CodigoCargo = 3,
                    CodigoCurriculoDisciplina = 2,
                    CodigoTurno = 2
                 },
                 new CargoDisciplina
                 {
                    CodigoCargo = 3,
                    CodigoCurriculoDisciplina = 2,
                    CodigoTurno = 3
                 },
                 new CargoDisciplina //disciplina a remover
                 {
                    CodigoCargo = 3,
                    CodigoCurriculoDisciplina = 2,
                    CodigoTurno = 1
                 },
                  new CargoDisciplina
                 {
                    CodigoCargo = 4,
                    CodigoCurriculoDisciplina = 4,
                    CodigoTurno = 2,
                    Descricao = "Substituindo nome disciplina no cargo"
                 },
                   new CargoDisciplina 
                 {
                    CodigoCargo = 4,
                    CodigoCurriculoDisciplina = 5,
                    CodigoTurno = 3
                 },
                    new CargoDisciplina 
                 {
                    CodigoCargo = 4,
                    CodigoCurriculoDisciplina = 6,
                    CodigoTurno = 1
                 }
            };

            _contexto.CargoDisciplina.AddRange(disciplinasCargo);
            _contexto.SaveChanges();
        }
    }
}
