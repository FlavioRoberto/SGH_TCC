using FastReport;
using FastReport.Export.Image;
using System.IO;

namespace SGH.Relatorios.Factories.Exportacao
{
    internal class ExportacaoJpg : IExportacao
    {
        public byte[] Exportar(Report relatorio)
        {
            using (var ms = new MemoryStream())
            {
                ImageExport image = new ImageExport();
                image.ImageFormat = ImageExportFormat.Jpeg;
                image.JpegQuality = 90;
                image.Resolution = 72;
                image.SeparateFiles = false;
                relatorio.Export(image, ms);
                return ms.ToArray();
            }
        }
    }
}
