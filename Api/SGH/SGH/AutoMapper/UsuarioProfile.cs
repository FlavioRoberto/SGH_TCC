using AutoMapper;
using Dominio.Model.Autenticacao;
using SGH.APi.ViewModel;
using SGH.Dominio.Model;

namespace SGH.Api.AutoMapper
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
