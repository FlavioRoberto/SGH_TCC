using FastReport;
using FastReport.Export.Image;

namespace SGH.Relatorios.Factories.Exportacao
{
    public class ExportacaoJpg : IExportacao
    {
        public void Exportar(Report relatorio, string nome)
        {
            ImageExport image = new ImageExport();
            image.ImageFormat = ImageExportFormat.Jpeg;
            image.JpegQuality = 90;
            image.Resolution = 72;
            image.SeparateFiles = false;
            relatorio.Export(image, $"{nome}.jpg");
        }
    }
}
