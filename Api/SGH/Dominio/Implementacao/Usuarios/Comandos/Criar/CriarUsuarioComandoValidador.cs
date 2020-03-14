using SGH.Data.Repositorio.Contratos;
using SGH.Dominio.Services.Contratos;

namespace SGH.Dominio.Services.Implementacao.Usuarios.Comandos.Criar
{
    public class CriarUsuarioComandoValidador : UsuarioComandoValidador<CriarUsuarioComando>, IValidador<CriarUsuarioComando>
    {

        public CriarUsuarioComandoValidador(IUsuarioRepositorio repositorio) :base(repositorio)
        {
        }

    }
}
