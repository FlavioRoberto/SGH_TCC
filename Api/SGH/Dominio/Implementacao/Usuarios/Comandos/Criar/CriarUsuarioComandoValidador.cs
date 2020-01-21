using SGH.Data.Repositorio.Contratos;
using SGH.Dominio.Contratos;

namespace SGH.Dominio.Implementacao.Usuarios.Comandos.Criar
{
    public class CriarUsuarioComandoValidador : UsuarioComandoValidador<CriarUsuarioComando>, ICriarUsuarioComandoValidador
    {

        public CriarUsuarioComandoValidador(IUsuarioRepositorio repositorio) :base(repositorio)
        {
        }

    }
}
