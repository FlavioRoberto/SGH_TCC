﻿using FluentValidation;

namespace SGH.Dominio.Services.Contratos
{
    public interface IValidador<T> : IValidator<T> 
    {
       string Validar(T comando);
    }
}
