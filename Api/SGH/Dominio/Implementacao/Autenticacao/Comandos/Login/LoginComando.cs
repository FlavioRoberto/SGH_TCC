﻿using MediatR;
using SGH.Dominio.Core;

namespace SGH.Dominio.Implementacao.Autenticacao.Comandos.Login
{
    public class LoginComando : IRequest<Resposta<string>>
    {
        public string Login { get; set; }
        public string Senha { get; set; }
    }
}
