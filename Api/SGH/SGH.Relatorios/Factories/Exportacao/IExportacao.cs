using FastReport;

namespace SGH.Relatorios.Factories.Exportacao
{
    internal interface IExportacao
    {
        void Exportar(Report relatorio, string nome);
    }
}
