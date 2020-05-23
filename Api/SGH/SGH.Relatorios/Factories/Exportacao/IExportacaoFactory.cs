using FastReport;

namespace SGH.Relatorios.Factories.Exportacao
{
    internal interface IExportacaoFactory
    {
        void Exportar(Report relatorio, string nome, ETipoExportacao tipo);
    }
}
