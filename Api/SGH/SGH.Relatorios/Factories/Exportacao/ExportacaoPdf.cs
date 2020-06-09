using FastReport;
using FastReport.Export.PdfSimple;
using System.IO;

namespace SGH.Relatorios.Factories.Exportacao
{
    internal class ExportacaoPdf : IExportacao
    {
        public byte[] Exportar(Report relatorio)
        {
            using(var ms = new MemoryStream())
            {
                PDFSimpleExport pdfExport = new PDFSimpleExport();
                pdfExport.Export(relatorio, ms);
                return ms.ToArray();
            }          
        }
    }
}
