using AutoMapper;
using SGH.Dominio.Core.Model;
using SGH.Dominio.Services.Implementacao.Cargos.Comandos.Atualizar;
using SGH.Dominio.Services.Implementacao.Cargos.Comandos.Criar;
using SGH.Dominio.ViewModel;

namespace SGH.Dominio.Services.AutoMapper
{
    public class CargoProfile : Profile
    {
        public CargoProfile()
        {
            CreateMap<CargoViewModel, Cargo>();
            CreateMap<Cargo, CargoViewModel>();
            CreateMap<Paginacao<CargoViewModel>, Paginacao<Cargo>>();
            CreateMap<Paginacao<Cargo>, Paginacao<CargoViewModel>>();
            CreateMap<Cargo, AtualizarCargoComando>();
            CreateMap<AtualizarCargoComando, Cargo>();
            CreateMap<Cargo, CriarCargoComando>();
            CreateMap<CriarCargoComando, Cargo>();
        }
    }
}
