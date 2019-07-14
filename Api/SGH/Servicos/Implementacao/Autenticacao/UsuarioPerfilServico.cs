using AutoMapper;
using Dominio.Model.Autenticacao;
using Dominio.ViewModel.AutenticacaoViewModel;
using Global;
using Repositorio;

namespace Servico.Implementacao.Autenticacao
{
    public class UsuarioPerfilServico : BaseService<UsuarioPerfilViewModel, UsuarioPerfil>
    {
        public UsuarioPerfilServico(IRepositorio<UsuarioPerfil> repositorio, IMapper mapper) : base(repositorio, mapper, "Perfil de usuário")
        { }

        protected override Resposta<UsuarioPerfilViewModel> ListarPeloCodigo(long id)
        {
            throw new System.NotImplementedException();
        }

        protected override Resposta<bool> RemoverPeloCodigo(long id)
        {
            throw new System.NotImplementedException();
        }
    }
}
