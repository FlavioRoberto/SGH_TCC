using FastReport;
using SGH.Relatorios.Contratos;
using SGH.Relatorios.Factories.Exportacao;
using System.IO;

namespace SGH.Relatorios.Implementacoes
{
    internal abstract class Relatorio<T> : IRelatorio<T> where T : IRelatorioData
    {
        private string NomeReport { get; set; }
        private readonly IExportacaoFactory _exportacaoFactory;
        protected abstract Report RegistrarParametros(Report report);
        protected abstract Report RegistrarDataSet(Report report);

        public Relatorio(string report)
        {
            NomeReport = report;
            _exportacaoFactory = new ExportacaoRelatorioFactory();
        }

        public byte[] Gerar()
        {
            var caminhoRepx = RetornarCaminhoReport();
            var relatorio = ConstruirRelatorio(caminhoRepx);
            return _exportacaoFactory.Exportar(relatorio, ETipoExportacao.PDF);
        }

        private Report ConstruirRelatorio(string caminhoRepx)
        {
            var relatorio = new Report();

            relatorio.Load(caminhoRepx);

            relatorio = RegistrarDataSet(relatorio);

            relatorio = RegistrarParametros(relatorio);

            relatorio.Prepare();

            relatorio.Save(caminhoRepx);

            return relatorio;
        }

        private string RetornarCaminhoReport()
        {
            var localizacao = System.Reflection.Assembly.GetEntryAssembly().Location;
            var diretorio = Path.GetDirectoryName(localizacao);
            return Path.Combine(diretorio, "Relatorios", NomeReport);
        }
    }
}