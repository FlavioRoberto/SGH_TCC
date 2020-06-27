
using SGH.Relatorios.DataSets;

namespace SGH.Relatorios.Contratos
{
    public interface IRelatorioServico
    {
        byte[] GerarRelatorioHorarioGeral(HorarioGeralRelatorioData dados);
        byte[] GerarRelatorioHorarioIndividual(HorarioIndividualRelatorioData dados);

    }
}
