namespace SGH.Relatorios.Contratos
{
    public interface IRelatorio<T> where T : IRelatorioData
    {
        void Gerar();
    }
}
