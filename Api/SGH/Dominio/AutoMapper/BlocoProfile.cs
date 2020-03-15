using AutoMapper;
using SGH.Dominio.Core.Model;
using SGH.Dominio.Services.Implementacao.Blocos.Comandos.Criar;
using SGH.Dominio.Services.ViewModel;

namespace SGH.Dominio.Services.AutoMapper
{
    public class BlocoProfile : Profile
    {
        public BlocoProfile()
        {
            CreateMap<Bloco, BlocoViewModel>();
            CreateMap<BlocoViewModel, Bloco>();

            CreateMap<Bloco, CriarBlocoComando>();
            CreateMap<CriarBlocoComando, Bloco>();
                       
        }
    }
}
