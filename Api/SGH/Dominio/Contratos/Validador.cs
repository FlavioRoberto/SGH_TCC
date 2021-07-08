using FluentValidation;
using System;

namespace SGH.Dominio.Services.Contratos
{
    public abstract class Validador<T> : AbstractValidator<T>, IValidador<T>
    {
        public string Validar(T comando)
        {
            var resultado = Validate(comando);

            if (!resultado.IsValid)
            {
                string erros = "";

                foreach (var erro in resultado.Errors)
                {
                    erros += erro.ErrorMessage + Environment.NewLine;
                }

                return erros;
            }

            return "";
        }
    }
}
