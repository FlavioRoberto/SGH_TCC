﻿using AutoMapper;
using SGH.APi.ViewModel;
using SGH.Dominio.Core.Model;

namespace SGH.Api.AutoMapper
{
    public class DisciplinaProfile : Profile
    {
        public DisciplinaProfile()
        {
            CreateMap<Disciplina, DisciplinaViewModel>();
            CreateMap<Paginacao<Disciplina>, Paginacao<DisciplinaViewModel>>();
            CreateMap<DisciplinaViewModel, Disciplina>();
            CreateMap<Paginacao<DisciplinaViewModel>, Paginacao<Disciplina>>();
        }
    }
}
