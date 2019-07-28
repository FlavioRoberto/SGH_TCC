using System.Threading.Tasks;
using Dominio.ViewModel.AutenticacaoViewModel;
using Global;

namespace Servico.Contratos
{
    public interface IUsuarioService : IServicoBase<UsuarioViewModel>
    {
        Task<Resposta<string>> Autenticar(LoginViewModel login);
        Task<Resposta<string>> RedefinirSenha(string email);
    }
}
