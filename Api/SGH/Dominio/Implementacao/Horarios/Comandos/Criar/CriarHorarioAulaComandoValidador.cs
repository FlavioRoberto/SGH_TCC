using SGH.Dominio.Core.Contratos;
using SGH.Dominio.Services.Implementacao.Horarios.Comandos.Comum;

namespace SGH.Dominio.Services.Implementacao.Horarios.Comandos.Criar
{
    public class CriarHorarioAulaComandoValidador : HorarioAulaComandoValidador<CriarHorarioAulaComando>
    {
        public CriarHorarioAulaComandoValidador(ITurnoRepositorio turnoRepositorio, 
                                               ICurriculoRepositorio curriculoRepositorio) : base(turnoRepositorio, curriculoRepositorio)
        {
        }
    }
}
