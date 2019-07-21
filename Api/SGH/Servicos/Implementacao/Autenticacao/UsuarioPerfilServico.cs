using AutoMapper;
using Dominio.Model.Autenticacao;
using Dominio.ViewModel.AutenticacaoViewModel;
using Repositorio;
using Servico.Contratos;

namespace Servico.Implementacao.Autenticacao
{
    public class UsuarioPerfilServico : BaseService<UsuarioPerfilViewModel, UsuarioPerfil>, IUsuarioPerfilService
    {
        public UsuarioPerfilServico(IRepositorio<UsuarioPerfil> repositorio, IMapper mapper) : base(repositorio, mapper, "Perfil de usuário")
        { }
    }
}
