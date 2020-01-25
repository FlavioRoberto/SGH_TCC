using AutoMapper;
using SGH.APi.ViewModel;
using SGH.Dominio.Core.Model;
using SGH.Dominio.Implementacao.Usuarios.Comandos.Atualizar;
using SGH.Dominio.Implementacao.Usuarios.Comandos.Criar;

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
