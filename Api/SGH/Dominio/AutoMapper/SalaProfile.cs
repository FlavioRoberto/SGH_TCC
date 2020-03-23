using AutoMapper;
using SGH.Dominio.Core.Model;
using SGH.Dominio.Services.Implementacao.Salas.Comandos.Criar;
using SGH.Dominio.Services.ViewModel;

namespace SGH.Dominio.Services.AutoMapper
{
    public class SalaProfile : Profile
    {
        public SalaProfile()
        {
            CreateMap<SalaViewModel, Sala>();
            CreateMap<Sala, SalaViewModel>();

            CreateMap<Sala, CriarSalaComando>();
            CreateMap<CriarSalaComando, Sala>();
        }
    }
}
