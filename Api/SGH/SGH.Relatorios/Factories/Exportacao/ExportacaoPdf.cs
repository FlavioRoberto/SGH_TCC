using FastReport;
using FastReport.Export.PdfSimple;


namespace SGH.Relatorios.Factories.Exportacao
{
    internal class ExportacaoPdf : IExportacao
    {
        public void Exportar(Report relatorio, string nome)
        {
            PDFSimpleExport pdfExport = new PDFSimpleExport();
            pdfExport.Export(relatorio, $"{nome}.pdf");
        }
    }
}
