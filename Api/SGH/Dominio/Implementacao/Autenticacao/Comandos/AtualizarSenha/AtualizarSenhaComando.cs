using MediatR;
using SGH.Dominio.Core;

namespace SGH.Dominio.Services.Implementacao.Autenticacao.Comandos.AtualizarSenha { 
    public class AtualizarSenhaComando : IRequest<Resposta<string>>
    {
        public string Senha { get; set; }
        public string NovaSenha { get; set; }
    }
}
