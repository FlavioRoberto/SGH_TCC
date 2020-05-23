using FastReport;
using SGH.Relatorios.Extensions;
using SGH.Relatorios.Factories.Exportacao;
using System.Collections.Generic;
using System.Data;
using System.IO;

namespace SGH.Relatorios.Implementacoes
{
    public class Teste
    {
        public string Titulo { get; private set; }

        public Teste(string titulo)
        {
            Titulo = titulo;
        }
    }

    public class TesteRelatorio
    {
        private readonly IExportacaoFactory _exportacaoFactory;

        public TesteRelatorio()
        {
            _exportacaoFactory = new ExportacaoRelatorioFactory();
        }

        public void Gerar()
        {
            var dataset = GetDataSet();
            var relatorio = new Report();
            relatorio.Load(Path.Combine("Relatorios", "Teste.frx"));
            relatorio.RegisterData(dataset.Tables["Teste"], "Teste");
            // enable it to use in a report
            relatorio.GetDataSource("Teste").Enabled = true;

            relatorio.GetDataSource("Teste");
            relatorio.Prepare();

            _exportacaoFactory.Exportar(relatorio, "Opa meu consagrado", ETipoExportacao.JPG);
        }

        private DataSet GetDataSet()
        {
            var lista = new List<Teste>() { new Teste("Hello relatorio") };
            return lista.ConverterParaDataSet("Data", "Teste");
        }    

    }
}
