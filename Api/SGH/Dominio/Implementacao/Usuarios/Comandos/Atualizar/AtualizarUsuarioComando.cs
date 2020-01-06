using MediatR;
using SGH.Dominio.Core;
using SGH.Dominio.Core.Model;

namespace SGH.Dominio.Implementacao.Usuarios.Comandos.Atualizar
{
    public class AtualizarUsuarioComando : IRequest<Resposta<Usuario>>
    {
        public int Codigo { get; set; }
        public string Nome { get; set; }
        public string Telefone { get; set; }
        public string Login { get; set; }
        public string Senha { get; set; }
        public string Email { get; set; }
        public int PerfilCodigo { get; set; }
        public string Foto { get; set; }
        public bool Ativo { get; set; }
    }
}
