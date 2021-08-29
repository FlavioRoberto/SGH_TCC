using SGH.Dominio.Core.DomainObjects.Datasets;
using SGH.Dominio.Core.Reports;
using System.IO;

namespace SGH.Relatorios.Implementacoes
{
    public class RelatorioServico : IRelatorioServico
    {
        public byte[] GerarRelatorioHorarioGeral(HorarioGeralRelatorioData dados)
        {
            return new HorarioGeralRelatorio(dados).Gerar();
        }

        public byte[] GerarRelatorioHorarioIndividual(HorarioIndividualRelatorioData dados)
        {
            return new HorarioIndividualRelatorio(dados).Gerar();
        }

        public MemoryStream GerarStreamRelatorioHorarioGeral(HorarioGeralRelatorioData dados)
        {
            return new HorarioGeralRelatorio(dados).GerarStream();
        }
    }
}
