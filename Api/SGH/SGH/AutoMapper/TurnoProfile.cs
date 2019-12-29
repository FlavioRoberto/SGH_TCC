using AutoMapper;
using Dominio.Model;
using SGH.APi.ViewModel;
using SGH.Dominio.Model;

namespace SGH.Api.AutoMapper
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
