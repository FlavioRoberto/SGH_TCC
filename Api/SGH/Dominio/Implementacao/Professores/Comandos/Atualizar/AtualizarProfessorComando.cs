using MediatR;
using SGH.Dominio.Core.Commands;using SGH.Dominio.Services.Contratos;
using SGH.Dominio.Core;
using SGH.Dominio.Core.Model;
using SGH.Dominio.Core.Enums;

namespace SGH.Dominio.Services.Implementacao.Professores.Comandos.Atualizar
{
    public class AtualizarProfessorComando : IRequest<Resposta<Professor>>, IProfessorComando
    {
        public int Codigo { get; set; }
        public string Matricula { get; set; }
        public string Nome { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }
        public bool? Ativo { get; set; }
        public EContratacao Contratacao { get; set; }
    }
}
