using SGH.Api.Testes.Factory.Contratos;
using SGH.Dominio.Core.Enums;
using SGH.Dominio.Core.Model;
using SHG.Data.Contexto;
using System.Collections.Generic;

namespace SGH.Api.Testes.Factory
{
    public class HorarioAulaTeste : IBancoTeste<HorarioAula>
    {
        private readonly IContexto _contexto;

        public HorarioAulaTeste(IContexto contexto)
        {
            _contexto = contexto;
        }

        public void InicializarBanco()
        {
            var horarios = new List<HorarioAula> { 
                new HorarioAula //1
                {
                    Ano = 2020,
                    CodigoCurriculo = 1,
                    CodigoTurno = 1,
                    Semestre = ESemestre.PRIMEIRO,
                    Periodo = EPeriodo.PRIMEIRO
                },
                new HorarioAula //2
                {
                    Ano = 2021,
                    CodigoCurriculo = 1,
                    CodigoTurno = 1,
                    Semestre = ESemestre.SEGUNDO,
                    Periodo = EPeriodo.SEGUNDO
                },
                new HorarioAula //3
                {
                    Ano = 2022,
                    CodigoCurriculo = 1,
                    CodigoTurno = 1,
                    Semestre = ESemestre.SEGUNDO,
                    Periodo = EPeriodo.TERCEIRO
                },
                new HorarioAula //4
                {
                    Ano = 2022,
                    CodigoCurriculo = 5,
                    CodigoTurno = 4,
                    Semestre = ESemestre.SEGUNDO,
                    Periodo = EPeriodo.TERCEIRO
                },
                 new HorarioAula //5
                {
                    Ano = 2022,
                    CodigoCurriculo = 4,
                    CodigoTurno = 2,
                    Semestre = ESemestre.SEGUNDO,
                    Periodo = EPeriodo.SEGUNDO
                },
                 new HorarioAula //6
                {
                    Ano = 2023,
                    CodigoCurriculo = 1,
                    CodigoTurno = 2,
                    Semestre = ESemestre.SEGUNDO,
                    Periodo = EPeriodo.SEGUNDO
                },
                new HorarioAula //7
                {
                    Ano = 2023,
                    CodigoCurriculo = 1,
                    CodigoTurno = 2,
                    Semestre = ESemestre.SEGUNDO,
                    Periodo = EPeriodo.TERCEIRO
                }
            };

            _contexto.HorarioAula.AddRange(horarios);
            _contexto.SaveChanges();
        }
    }
}
