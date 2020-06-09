namespace SGH.Relatorios.Contratos
{
    internal interface IRelatorio<T> where T : IRelatorioData
    {
        byte[] Gerar();
    }
}
