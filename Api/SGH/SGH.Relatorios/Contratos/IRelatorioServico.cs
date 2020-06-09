
using SGH.Relatorios.DataSets;

namespace SGH.Relatorios.Contratos
{
    public interface IRelatorioServico
    {
        byte[] GerarRelatorioHorario(HorarioRelatorioData dados);
    }
}
