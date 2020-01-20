using FluentValidation;
using SGH.Dominio.Contratos;

namespace SGH.Dominio.Implementacao.Professores.Comandos.Remover
{
    public class RemoverProfessorComandoValidador : AbstractValidator<RemoverProfessorComando>, IRemoverProfessorComandoValidador
    {
        public RemoverProfessorComandoValidador()
        {

        }
    }
}
