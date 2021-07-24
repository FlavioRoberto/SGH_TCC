using FastReport;
using System.IO;

namespace SGH.Relatorios.Factories.Exportacao
{
    internal interface IExportacao
    {
        byte[] Exportar(Report relatorio);
        MemoryStream ExportarStream(Report relatorio);
    }
}
