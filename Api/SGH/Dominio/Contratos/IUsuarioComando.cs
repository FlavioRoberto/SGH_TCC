﻿namespace SGH.Dominio.Services.Contratos
{
    public interface IUsuarioComando
    {
        int? Codigo { get; set; }
        string Email { get; set; }
        string Login { get; set; }
        string Nome { get; set; }
        int PerfilCodigo { get; set; }
        int CursoCodigo { get; set; }
    }
}
