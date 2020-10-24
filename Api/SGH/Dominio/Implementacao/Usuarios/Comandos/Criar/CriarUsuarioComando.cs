using MediatR;
using SGH.Dominio.Services.Contratos;
using SGH.Dominio.Core;
using SGH.Dominio.Core.Model;

namespace SGH.Dominio.Services.Implementacao.Usuarios.Comandos.Criar
{
    public class CriarUsuarioComando : IRequest<Resposta<Usuario>>, IUsuarioComando
    {
        public int? Codigo { get; set; }
        public string Nome { get; set; }
        public string Telefone { get; set; }
        public string Senha { get; set; }
        public int PerfilCodigo { get; set; }
        public string Foto { get; set; }
        public bool Ativo { get; set; }
        public string Email { get; set; }
        public string Login { get; set; }
        public int? CursoCodigo { get; set; }
    }
}
