using FluentValidation;
using SGH.Data.Repositorio.Contratos;
using SGH.Dominio.Services.Implementacao.Turnos.Comandos.Base;
using System;
using System.Threading;
using System.Threading.Tasks;
using SGH.Dominio.Shared.Extensions;

namespace SGH.Dominio.Services.Implementacao.Turnos.Comandos.Atualizar
{
    public class AtualizarTurnoComandoValidador : TurnoCursoComandoValidador<AtualizarTurnoComando>
    {
        private readonly ITurnoRepositorio _repositorio;

        public AtualizarTurnoComandoValidador(ITurnoRepositorio repositorio)
        {
            _repositorio = repositorio;
            
            RuleFor(lnq => lnq.Codigo)
                .NotEmpty()
                .WithMessage("O código do turno não pode ser vazio.");

            When(lnq => lnq.Codigo.HasValue, () =>
            {
                RuleFor(lnq => lnq)
                    .MustAsync(ValidarSeDescricaoExiste)
                    .WithMessage("Não foi possível atualizar o turno, pois já existe um turno com a descrição informada.");
            });
        }

        private async Task<bool> ValidarSeDescricaoExiste(AtualizarTurnoComando comando, CancellationToken arg2)
        {
            var existeDescricao = await _repositorio.Contem(lnq => lnq.Descricao.IgualA(comando.Descricao) &&
                                                                   lnq.Codigo != comando.Codigo);

            if (existeDescricao)
                return false;

            return true;
        }
    }
}
