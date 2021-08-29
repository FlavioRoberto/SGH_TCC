namespace SGH.Dominio.Core.Reports
{
    public interface IRelatorio<T> where T : IRelatorioData
    {
        byte[] Gerar();
    }
}
