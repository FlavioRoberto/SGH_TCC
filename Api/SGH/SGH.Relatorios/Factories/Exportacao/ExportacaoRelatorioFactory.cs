using FastReport;

namespace SGH.Relatorios.Factories.Exportacao
{
    internal class ExportacaoRelatorioFactory : IExportacaoFactory
    {
        private IExportacao _exportacao;

        public void Exportar(Report relatorio, string nome, ETipoExportacao tipo)
        {
            switch (tipo)
            {
                case ETipoExportacao.JPG: _exportacao = new ExportacaoJpg(); break;
                case ETipoExportacao.PDF: _exportacao = new ExportacaoPdf();break;
            }

            _exportacao.Exportar(relatorio, nome);
        }
    }
}
