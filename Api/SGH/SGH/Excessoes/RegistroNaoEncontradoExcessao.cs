using System;

namespace Api.Excessoes
{
    public class RegistroNaoEncontradoExcecao : Exception
    {
        public RegistroNaoEncontradoExcecao(string nome, object chave)
            : base($"O registro ({chave}) da entidade \"{nome}\" não foi encontrado.")
        {
        }
    }
}
