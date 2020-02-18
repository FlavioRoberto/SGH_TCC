using AutoMapper;
using SGH.Dominio.Core.Model;
using SGH.Dominio.Implementacao.CargosDisciplinas.Comandos.Criar;
using SGH.Dominio.ViewModel;

namespace SGH.Dominio.AutoMapper
{
    public class CargoDisciplinaProfile : Profile
    {
        public CargoDisciplinaProfile()
        {
            CreateMap<CargoDisciplinaViewModel, CargoDisciplina>();
            CreateMap<CargoDisciplina, CargoDisciplinaViewModel>();
            CreateMap<CargoDisciplina, CriarCargoDisciplinaComando>();
            CreateMap<CriarCargoDisciplinaComando, CargoDisciplina>();
        }
    }
}
