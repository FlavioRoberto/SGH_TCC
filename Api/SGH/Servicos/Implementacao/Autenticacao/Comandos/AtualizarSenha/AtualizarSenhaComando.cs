using Global;
using MediatR;

namespace Aplicacao.Implementacao.Autenticacao.Comandos.AtualizarSenha
{
    public class AtualizarSenhaComando : IRequest<Resposta<string>>
    {
        public string Senha { get; set; }
        public string NovaSenha { get; set; }
    }
}
