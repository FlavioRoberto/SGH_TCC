using FastReport;

namespace SGH.Relatorios.Factories.Exportacao
{
    internal interface IExportacaoFactory
    {
        byte[] Exportar(Report relatorio, ETipoExportacao tipo);
    }
}
