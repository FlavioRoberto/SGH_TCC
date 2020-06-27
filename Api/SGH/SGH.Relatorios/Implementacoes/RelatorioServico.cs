using SGH.Relatorios.Contratos;
using SGH.Relatorios.DataSets;

namespace SGH.Relatorios.Implementacoes
{
    internal class RelatorioServico : IRelatorioServico
    {
        public byte[] GerarRelatorioHorarioGeral(HorarioGeralRelatorioData dados)
        {
            return new HorarioGeralRelatorio(dados).Gerar();
        }

        public byte[] GerarRelatorioHorarioIndividual(HorarioIndividualRelatorioData dados)
        {
            return new HorarioIndividualRelatorio(dados).Gerar();
        }
    }
}
