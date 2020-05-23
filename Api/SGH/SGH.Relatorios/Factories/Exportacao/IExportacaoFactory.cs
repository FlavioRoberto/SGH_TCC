using FastReport;

namespace SGH.Relatorios.Factories.Exportacao
{
    public interface IExportacaoFactory
    {
        void Exportar(Report relatorio, string nome, ETipoExportacao tipo);
    }
}
