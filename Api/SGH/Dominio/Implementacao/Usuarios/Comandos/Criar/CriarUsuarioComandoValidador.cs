using SGH.Dominio.Core.Commands;using SGH.Dominio.Services.Contratos;
using SGH.Dominio.Core.Repositories;

namespace SGH.Dominio.Services.Implementacao.Usuarios.Comandos.Criar
{
    public class CriarUsuarioComandoValidador : UsuarioComandoValidador<CriarUsuarioComando>, IValidador<CriarUsuarioComando>
    {

        public CriarUsuarioComandoValidador(IUsuarioRepositorio repositorio, ICursoRepositorio cursoRepositorio) :base(repositorio, cursoRepositorio)
        {
        }

    }
}
