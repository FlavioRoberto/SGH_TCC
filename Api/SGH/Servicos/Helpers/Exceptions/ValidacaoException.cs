using System;

namespace Aplicacao.Exceptions
{
    public class ValidacaoException : Exception
    {
        public ValidacaoException(string mensagem) : base(mensagem)
        {

        }
    }
}
