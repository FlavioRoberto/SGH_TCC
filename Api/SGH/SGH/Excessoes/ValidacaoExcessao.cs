using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Api.Excessoes
{
    public class ValidacaoExcecao : Exception
    {
        public ValidacaoExcecao()
            : base("Uma ou mais validações ocorreram.")
        {
            Validacoes = new Dictionary<string, string[]>();
        }

        public ValidacaoExcecao(List<ValidationFailure> validacoes)
            : this()
        {
            Validacoes.Add("error", validacoes.Select(e => e.ErrorMessage).ToArray());
        }

        public IDictionary<string, string[]> Validacoes { get; }
    }
}
