﻿namespace SGH.Dominio.Core.Commands
{
    public interface IUsuarioComando
    {
        long? Codigo { get; set; }
        string Email { get; set; }
        string Login { get; set; }
        string Nome { get; set; }
        long PerfilCodigo { get; set; }
        long? CursoCodigo { get; set; }
    }
}
