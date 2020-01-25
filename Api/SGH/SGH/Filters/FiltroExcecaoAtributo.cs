﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using SGH.Api.Excessoes;
using System.Net;

namespace SGH.Api.Filters
{
    public class FiltroExcecaoAtributo : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            if (context.Exception is ValidacaoExcecao)
            {
                context.HttpContext.Response.ContentType = "application/json";
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                context.Result = new JsonResult(
                    ((ValidacaoExcecao)context.Exception).Validacoes);

                return;
            }

            var code = HttpStatusCode.InternalServerError;

            if (context.Exception is RegistroNaoEncontradoExcecao)
                code = HttpStatusCode.NotFound;

            context.HttpContext.Response.ContentType = "application/json";
            context.HttpContext.Response.StatusCode = (int)code;
            context.Result = new JsonResult(new
            {
                error = new[] { context.Exception.Message },
                stackTrace = context.Exception.StackTrace
            });
        }

    }

}
