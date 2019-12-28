using FluentValidation;
using Servico.Contratos;
using System;
using System.ComponentModel.DataAnnotations;

namespace Servico.Extensions
{
    public static class AbstractValidationExtension
    {
        public static string Validar(this IValidator value, IComando comando)
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
