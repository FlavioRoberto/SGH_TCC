using FluentValidation;
using System;
using MediatR;

namespace SGH.Dominio.Services.Extensions
{
    public static class AbstractValidationExtension
    {
        public static string Validar<T>(this IValidator value, IRequest<T> comando)
        {
            var resultado = value.Validate(comando);

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
