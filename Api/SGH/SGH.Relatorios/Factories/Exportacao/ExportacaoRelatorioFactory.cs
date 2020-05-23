using FastReport;

namespace SGH.Relatorios.Factories.Exportacao
{
    public class ExportacaoRelatorioFactory : IExportacaoFactory
    {
        private IExportacao _exportacao;

        public void Exportar(Report relatorio, string nome, ETipoExportacao tipo)
        {
            switch (tipo)
            {
                case ETipoExportacao.JPG: _exportacao = new ExportacaoJpg(); break;
            }

            _exportacao.Exportar(relatorio, nome);
        }
    }
}
