using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;

namespace SGH.Relatorios.Extensions
{
    internal static class DataSetExtension
    {
        public static DataSet ConverterParaDataSet<T>(this IEnumerable<T> lista, string nomeDataset, string nomeTabela)
        {
            if (lista == null)
                throw new ArgumentNullException("source ");

            if (string.IsNullOrEmpty(nomeTabela))
                throw new ArgumentNullException("nomeTabela");

            if (string.IsNullOrEmpty(nomeDataset))
                throw new ArgumentNullException("nomeDataset");

            var converted = new DataSet(nomeDataset);
            converted.Tables.Add(NovaTabela(nomeTabela, lista));
            return converted;
        }

        private static DataTable NovaTabela<T>(string name, IEnumerable<T> list)
        {
            PropertyInfo[] propInfo = typeof(T).GetProperties();
            DataTable table = CriarTabela<T>(name, list, propInfo);
            IEnumerator<T> enumerator = list.GetEnumerator();
            while (enumerator.MoveNext())
                table.Rows.Add(CriarLinha<T>(table.NewRow(), enumerator.Current, propInfo));
            return table;
        }

        private static DataRow CriarLinha<T>(DataRow row, T listItem, PropertyInfo[] pi)
        {
            foreach (PropertyInfo p in pi)
                row[p.Name.ToString()] = p.GetValue(listItem, null);
            return row;
        }

        private static DataTable CriarTabela<T>(string name, IEnumerable<T> list, PropertyInfo[] pi)
        {
            DataTable table = new DataTable(name);
            foreach (PropertyInfo p in pi)
                table.Columns.Add(p.Name, p.PropertyType);
            return table;
        }
    }
}
