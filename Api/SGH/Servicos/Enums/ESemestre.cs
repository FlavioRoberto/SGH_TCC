﻿using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SGH.Dominio.Core.Enums
{
    public enum ESemestre
    {
        [Description("1° Semestre")]
        PRIMEIRO = 1,
        [Description("2° Semestre")]
        SEGUNDO = 2
    }
}
