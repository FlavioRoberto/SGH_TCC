namespace Servico
{
    public class Resposta<T>
    {
        private string _erro;
        private T _resultado;

        public Resposta(T entidade, string erro)
        {
            _erro = erro;
            _resultado = entidade;    
        }

        public Resposta(T entidade)
        {
            _resultado = entidade;
            _erro = string.Empty;
        }

        public string GetErros()
        {
            return _erro;
        }

        public T GetResultado()
        {
            return _resultado;
        }

        public bool TemErro()
        {
            return _erro.Length > 0;
        }

    }
}
