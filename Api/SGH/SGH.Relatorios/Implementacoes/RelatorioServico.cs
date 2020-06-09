using SGH.Relatorios.Contratos;
using SGH.Relatorios.DataSets;

namespace SGH.Relatorios.Implementacoes
{
    internal class RelatorioServico : IRelatorioServico
    {
        public byte[] GerarRelatorioHorario(HorarioRelatorioData dados)
        {
            return new HorarioRelatorio(dados).Gerar();
        }
    }
}
