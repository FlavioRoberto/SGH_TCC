using AutoMapper;
using SGH.Dominio.ViewModel;
using SGH.Dominio.Core.Model;

namespace SGH.Dominio.AutoMapper
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
