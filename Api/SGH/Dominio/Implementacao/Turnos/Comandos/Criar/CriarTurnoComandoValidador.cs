using FluentValidation;
using SGH.Dominio.Core.Repositories;
using SGH.Dominio.Services.Implementacao.Turnos.Comandos.Base;
using System.Threading;
using System.Threading.Tasks;
using SGH.Dominio.Shared.Extensions;

namespace SGH.Dominio.Services.Implementacao.Turnos.Comandos.Criar
{
    public class CriarTurnoComandoValidador : TurnoCursoComandoValidador<CriarTurnoComando>
    {
        private readonly ITurnoRepositorio _turnoRepositorio;

        public CriarTurnoComandoValidador(ITurnoRepositorio turnoRepositorio)
        {
            _turnoRepositorio = turnoRepositorio;

            RuleFor(lnq => lnq.Descricao)
                .MustAsync(ValidarSeDescricaoExiste)
                .WithMessage("Não foi possível criar o turno, pois já existe um turno com a descrição informada.");
        }

        private async Task<bool> ValidarSeDescricaoExiste(string descricao, CancellationToken arg2)
        {
            var existeDescricao = await _turnoRepositorio.Contem(lnq => lnq.Descricao.IgualA(descricao));

            if (existeDescricao)
                return false;

            return true;
        }
    }
}
