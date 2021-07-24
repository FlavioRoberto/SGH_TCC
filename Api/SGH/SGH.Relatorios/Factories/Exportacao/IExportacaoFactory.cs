using FastReport;
using System.IO;

namespace SGH.Relatorios.Factories.Exportacao
{
    internal interface IExportacaoFactory
    {
        byte[] Exportar(Report relatorio, ETipoExportacao tipo);
        MemoryStream ExportarStream(Report relatorio, ETipoExportacao tipo);
    }
}
