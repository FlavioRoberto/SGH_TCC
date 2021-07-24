
using SGH.Relatorios.DataSets;
using System.IO;

namespace SGH.Relatorios.Contratos
{
    public interface IRelatorioServico
    {
        MemoryStream GerarStreamRelatorioHorarioGeral(HorarioGeralRelatorioData dados);
        byte[] GerarRelatorioHorarioGeral(HorarioGeralRelatorioData dados);
        byte[] GerarRelatorioHorarioIndividual(HorarioIndividualRelatorioData dados);

    }
}
