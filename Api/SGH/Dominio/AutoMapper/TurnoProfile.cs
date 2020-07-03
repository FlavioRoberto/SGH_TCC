using AutoMapper;
using SGH.Dominio.ViewModel;
using SGH.Dominio.Core.Model;
using System.Linq;
using System.Collections.Generic;
using System;
using SGH.Dominio.Core;

namespace SGH.Dominio.Services.AutoMapper
{
    public class TurnoProfile : Profile
    {
        public TurnoProfile()
        {
            CreateMap<Turno, TurnoViewModel>()
                .ForMember(DTO => DTO.Horarios, opt => opt.MapFrom(lnq => !string.IsNullOrEmpty(lnq.Horarios) ?
                                                                          lnq.Horarios.Split(',', StringSplitOptions.RemoveEmptyEntries)
                                                                             .ToList() : new List<string>()));
            CreateMap<TurnoViewModel, Turno>()
                .ForMember(DTO => DTO.Horarios, opt => opt.MapFrom(lnq => string.Join(",", lnq.Horarios)));

            CreateMap<Resposta<Turno>, Resposta<TurnoViewModel>>().ReverseMap();

            CreateMap<Paginacao<Turno>, Paginacao<TurnoViewModel>>().ReverseMap();
        }
    }
}
