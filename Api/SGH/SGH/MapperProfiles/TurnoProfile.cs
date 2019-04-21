using AutoMapper;
using Dominio.Model;
using Dominio.ViewModel;

namespace Api.MappersProfiles
{
    public class TurnoProfile : Profile
    {
        public TurnoProfile()
        {
            CreateMap<Turno, TurnoViewModel>();
            CreateMap<Paginacao<Turno>, Paginacao<TurnoViewModel>>();
            CreateMap<TurnoViewModel, Turno>();
            CreateMap<Paginacao<TurnoViewModel>, Paginacao<Turno>>();
        }
    }
}
