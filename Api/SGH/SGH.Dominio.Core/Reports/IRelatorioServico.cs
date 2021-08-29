
using SGH.Dominio.Core.DomainObjects.Datasets;
using System.IO;

namespace SGH.Dominio.Core.Reports
{
    public interface IRelatorioServico
    {
        MemoryStream GerarStreamRelatorioHorarioGeral(HorarioGeralRelatorioData dados);
        byte[] GerarRelatorioHorarioGeral(HorarioGeralRelatorioData dados);
        byte[] GerarRelatorioHorarioIndividual(HorarioIndividualRelatorioData dados);

    }
}
