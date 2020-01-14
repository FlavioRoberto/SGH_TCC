using MediatR;
using SGH.Dominio.Contratos;
using SGH.Dominio.Core;
using SGH.Dominio.Core.Model;

namespace SGH.Dominio.Implementacao.Professores.Comandos.Criar
{
    public class CriarProfessorComando : IRequest<Resposta<Professor>>, IProfessorComando
    {
        public string Matricula { get; set; }
        public string Nome { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }
        public bool? Ativo { get; set; }
    }
}
