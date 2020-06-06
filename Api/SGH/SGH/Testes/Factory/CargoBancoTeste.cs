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
                },
                new Cargo //5
                {
                    Ano = 2020,
                    CodigoProfessor = 2,
                    Edital = 13,
                    Semestre = ESemestre.PRIMEIRO
                },
                new Cargo //6
                {
                    Ano = 2021,
                    CodigoProfessor = 3,
                    Edital = 14,
                    Semestre = ESemestre.PRIMEIRO
                },
                new Cargo //7
                {
                    Ano = 2021,
                    CodigoProfessor = 3,
                    Edital = 14,
                    Semestre = ESemestre.PRIMEIRO
                },
                new Cargo //8 - Cargo remover
                {
                    Ano = 2021,
                    CodigoProfessor = 5,
                    Edital = 14,
                    Semestre = ESemestre.PRIMEIRO
                }
            };

            _contexto.Cargo.AddRange(cargos);
            _contexto.SaveChanges();

            var disciplinasCargo = new List<CargoDisciplina> {
                 new CargoDisciplina //1
                 {
                    CodigoCargo = 1,
                    CodigoCurriculoDisciplina = 1,
                    CodigoTurno = 1
                 },
                 new CargoDisciplina //2
                 {
                    CodigoCargo = 2,
                    CodigoCurriculoDisciplina = 1,
                    CodigoTurno = 2
                 },
                  new CargoDisciplina //3
                 {
                    CodigoCargo = 2,
                    CodigoCurriculoDisciplina = 2,
                    CodigoTurno = 3
                 },
                  new CargoDisciplina //4
                 {
                    CodigoCargo = 3,
                    CodigoCurriculoDisciplina = 1,
                    CodigoTurno = 1
                 },
                  new CargoDisciplina //5
                 {
                    CodigoCargo = 3,
                    CodigoCurriculoDisciplina = 2,
                    CodigoTurno = 2
                 },
                 new CargoDisciplina //6
                 {
                    CodigoCargo = 3,
                    CodigoCurriculoDisciplina = 2,
                    CodigoTurno = 3
                 },
                 new CargoDisciplina // 7 disciplina a remover
                 {
                    CodigoCargo = 3,
                    CodigoCurriculoDisciplina = 2,
                    CodigoTurno = 1
                 },
                  new CargoDisciplina //8
                 {
                    CodigoCargo = 4,
                    CodigoCurriculoDisciplina = 4,
                    CodigoTurno = 2,
                    Descricao = "Substituindo nome disciplina no cargo"
                 },
                 new CargoDisciplina //9
                 {
                    CodigoCargo = 4,
                    CodigoCurriculoDisciplina = 5,
                    CodigoTurno = 3
                 },
                 new CargoDisciplina //10
                 {
                    CodigoCargo = 4,
                    CodigoCurriculoDisciplina = 6,
                    CodigoTurno = 1
                 },
                 new CargoDisciplina //11
                 {
                    CodigoCargo = 5,
                    CodigoCurriculoDisciplina = 1,
                    CodigoTurno = 2
                 },
                 new CargoDisciplina //12
                 {
                    CodigoCargo = 5,
                    CodigoCurriculoDisciplina = 2,
                    CodigoTurno = 2
                 },
                 new CargoDisciplina //13
                 {
                    CodigoCargo = 6,
                    CodigoCurriculoDisciplina = 2,
                    CodigoTurno = 2
                 },
                 new CargoDisciplina //14
                 {
                    CodigoCargo = 7,
                    CodigoCurriculoDisciplina = 1,
                    CodigoTurno = 2
                 }
            };

            _contexto.CargoDisciplina.AddRange(disciplinasCargo);
            _contexto.SaveChanges();
        }
    }
}
