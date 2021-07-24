using FastReport;
using System.IO;

namespace SGH.Relatorios.Factories.Exportacao
{
    internal class ExportacaoRelatorioFactory : IExportacaoFactory
    {

        public byte[] Exportar(Report relatorio, ETipoExportacao tipo)
        {
            var exportacao = DefinirExportacao(tipo);
            return exportacao.Exportar(relatorio);
        }

        public MemoryStream ExportarStream(Report relatorio, ETipoExportacao tipo)
        {
            var exportacao = DefinirExportacao(tipo);
            return exportacao.ExportarStream(relatorio);
        }

        private IExportacao DefinirExportacao(ETipoExportacao tipo)
        {
            switch (tipo)
            {
                case ETipoExportacao.JPG:  return new ExportacaoJpg();
                default: return new ExportacaoPdf(); 
            }
        }
    }
}
