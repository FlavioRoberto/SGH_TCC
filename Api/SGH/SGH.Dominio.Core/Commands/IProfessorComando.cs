using SGH.Dominio.Core.Enums;

namespace SGH.Dominio.Core.Commands
{
    public interface IProfessorComando
    {
        string Matricula { get; set; }
        string Nome { get; set; }
        string Telefone { get; set; }
        string Email { get; set; }
        bool? Ativo { get; set; }
        EContratacao Contratacao { get; set; }

    }
}
