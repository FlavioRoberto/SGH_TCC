﻿using FluentValidation;
using SGH.Data.Repositorio.Contratos;
using SGH.Dominio.Contratos;
using SGH.Dominio.Implementacao.Cargos.Comandos.Base;
using System.Threading;
using System.Threading.Tasks;

namespace SGH.Dominio.Implementacao.Cargos.Comandos.Criar
{
    public class CriarCargoComandoValidador : CargoComandoValidadorBase<CriarCargoComando>, ICriarCargoComandoValidador
    {      
        private readonly ICargoRepositorio _cargoRepositorio;

        public CriarCargoComandoValidador(IProfessorRepositorio professorRepositorio, ICargoRepositorio cargoRepositorio): base(professorRepositorio)
        {
            _cargoRepositorio = cargoRepositorio;

            RuleFor(lnq => lnq).MustAsync(ValidarSeCargoJaCadastrado).WithMessage("Já existe um cargo com os mesmos valores para os campos semestre, ano, edital e número.");
        }

        private async Task<bool> ValidarSeCargoJaCadastrado(CriarCargoComando comando, CancellationToken arg2)
        {
            var resultado = await _cargoRepositorio.Contem(lnq => lnq.Semestre == comando.Semestre && 
                                                           lnq.Ano == comando.Ano && 
                                                           lnq.Edital == comando.Edital && 
                                                           lnq.Numero == comando.Numero);
            return !resultado;
        }
    }
}
