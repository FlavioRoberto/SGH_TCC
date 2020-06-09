using FastReport;

namespace SGH.Relatorios.Factories.Exportacao
{
    internal interface IExportacao
    {
        byte[] Exportar(Report relatorio);
    }
}
