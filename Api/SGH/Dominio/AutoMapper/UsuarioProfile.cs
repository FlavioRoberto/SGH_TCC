using AutoMapper;
using SGH.Dominio.ViewModel;
using SGH.Dominio.Core.Model;

namespace SGH.Dominio.Services.AutoMapper
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
