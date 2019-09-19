using System;

namespace Servico.Exceptions
{
    public class ValidacaoException : Exception
    {
        public ValidacaoException(string mensagem) : base(mensagem)
        {

        }
    }
}
