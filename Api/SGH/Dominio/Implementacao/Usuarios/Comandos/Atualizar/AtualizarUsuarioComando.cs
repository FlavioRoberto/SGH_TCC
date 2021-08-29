using MediatR;
using SGH.Dominio.Core.Commands;using SGH.Dominio.Services.Contratos;
using SGH.Dominio.Core;
using SGH.Dominio.Core.Model;

namespace SGH.Dominio.Services.Implementacao.Usuarios.Comandos.Atualizar
{
    public class AtualizarUsuarioComando : IUsuarioComando, IRequest<Resposta<Usuario>>
    {
        public long? Codigo { get; set; }
        public string Nome { get; set; }
        public string Telefone { get; set; }
        public string Senha { get; set; }
        public long PerfilCodigo { get; set; }
        public string Foto { get; set; }
        public bool Ativo { get; set; }
        public string Email { get; set; }
        public string Login { get; set; }
        public long? CursoCodigo { get; set; }
    }
}
