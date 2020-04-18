using SGH.Api.Testes.Factory.Contratos;
using SGH.Dominio.Core.Model;
using SHG.Data.Contexto;
using System;
using System.Collections.Generic;

namespace SGH.Api.Testes.Factory
{
    public class CurriculoBancoTeste : IBancoTeste<Curriculo>
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
                },
                 new Curriculo
                {
                    Ano = DateTime.Now.Year + 1,
                    CodigoCurso = 2 //ENGENHARIA CIVIL
                },
                  new Curriculo
                {
                    Ano = DateTime.Now.Year + 2,
                    CodigoCurso = 3 //ENGENHARIA DE PRODUCAO
                },
                new Curriculo
                {
                    Ano = DateTime.Now.Year + 2,
                    CodigoCurso = 3 //ENGENHARIA DE PRODUCAO
                },
                //5 - teste remover vinculado horário
                new Curriculo
                {
                    Ano = DateTime.Now.Year + 2,
                    CodigoCurso = 3 
                }
            };

            _contexto.Curriculo.AddRange(curriculos);
            _contexto.SaveChanges();

            var disciplinasCurriculo = new List<CurriculoDisciplina>
            {
                new CurriculoDisciplina  //1
                {
                    CodigoCurriculo = 1,
                    CodigoDisciplina = 1
                },
                new CurriculoDisciplina //1
                {
                    CodigoCurriculo = 1,
                    CodigoDisciplina = 2
                },
                new CurriculoDisciplina //3
                {
                    CodigoCurriculo = 1,
                    CodigoDisciplina = 3
                },
                new CurriculoDisciplina  //4
                {
                    CodigoCurriculo = 1, //Engenharia da Computação
                    CodigoDisciplina = 3
                },
                new CurriculoDisciplina //5
                {
                    CodigoCurriculo = 2, //Engenharia Civil
                    CodigoDisciplina = 4
                },
                new CurriculoDisciplina //6
                {
                    CodigoCurriculo = 3, //engenharia de produção
                    CodigoDisciplina = 5
                },
                 new CurriculoDisciplina //7
                {
                    CodigoCurriculo = 2, //Engenharia Civil
                    CodigoDisciplina = 5
                },
                new CurriculoDisciplina  //8
                {
                    CodigoCurriculo = 1, //disciplina a remover
                    CodigoDisciplina = 5
                },
                new CurriculoDisciplina //9
                {
                    CodigoCurriculo = 4, //Engenharia Civil
                    CodigoDisciplina = 4
                },
                new CurriculoDisciplina //10
                {
                    CodigoCurriculo = 4, //engenharia de produção
                    CodigoDisciplina = 5
                },
            };

            _contexto.CurriculoDisciplina.AddRange(disciplinasCurriculo);
            _contexto.SaveChanges();

            var disciplinasCurriculoPreRequisito = new List<CurriculoDisciplinaPreRequisito> {
                new CurriculoDisciplinaPreRequisito //preRequisito disciplina a remover
                {
                    CodigoCurriculoDisciplina = 7,
                    CodigoDisciplina = 1
                },
                new CurriculoDisciplinaPreRequisito
                {
                    CodigoCurriculoDisciplina = 9,
                    CodigoDisciplina = 2
                },
                new CurriculoDisciplinaPreRequisito
                {
                    CodigoCurriculoDisciplina = 9,
                    CodigoDisciplina = 1
                }
            };

            _contexto.CurriculoDisciplinaPreRequisito.AddRange(disciplinasCurriculoPreRequisito);
            _contexto.SaveChanges();
        }
    }
}
