using MediatR;
using SGH.Dominio.Contratos;
using SGH.Dominio.Core;
using SGH.Dominio.Core.Model;

namespace SGH.Dominio.Implementacao.Professores.Comandos.Atualizar
{
    public class AtualizarProfessorComando : IRequest<Resposta<Professor>>, IProfessorComando
    {
        public int ProfessorId { get; set; }
        public string Matricula { get; set; }
        public string Nome { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }
        public bool? Ativo { get; set; }
    }
}
