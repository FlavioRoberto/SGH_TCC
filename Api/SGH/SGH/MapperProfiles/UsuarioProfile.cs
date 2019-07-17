using AutoMapper;
using Dominio.Model.Autenticacao;
using Dominio.ViewModel;
using Dominio.ViewModel.AutenticacaoViewModel;

namespace Api.MapperProfiles
{
    public class UsuarioProfile : Profile
    {
        public UsuarioProfile()
        {
            CreateMap<Usuario, UsuarioViewModel>();
            CreateMap<UsuarioViewModel, Usuario>();
            CreateMap<Paginacao<Usuario>, Paginacao<UsuarioViewModel>>();
            CreateMap<Paginacao<UsuarioViewModel>, Paginacao<Usuario>>();
        }
    }
}
