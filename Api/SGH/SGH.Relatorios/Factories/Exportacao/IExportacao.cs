using FastReport;

namespace SGH.Relatorios.Factories.Exportacao
{
    public interface IExportacao
    {
        void Exportar(Report relatorio, string nome);
    }
}
