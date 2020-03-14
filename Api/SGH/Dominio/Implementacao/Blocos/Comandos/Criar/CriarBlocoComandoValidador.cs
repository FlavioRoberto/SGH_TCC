using FluentValidation;
using SGH.Dominio.Services.Contratos;

namespace SGH.Dominio.Services.Implementacao.Blocos.Comandos.Criar
{
    public class CriarBlocoComandoValidador : AbstractValidator<CriarBlocoComando>, IValidador<CriarBlocoComando>
    {
    }
}
