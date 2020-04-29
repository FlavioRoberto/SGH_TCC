using AutoMapper;
using SGH.Dominio.Core.Model;
using SGH.Dominio.Services.Implementacao.Aulas.Comandos.Criar;
using SGH.Dominio.Services.Implementacao.Aulas.ViewModels;

namespace SGH.Dominio.Services.AutoMapper
{
    public class AulaProfile : Profile
    {
        public AulaProfile()
        {
            CreateMap<Aula, AulaViewModel>().ReverseMap();
            CreateMap<Aula, CriarAulaComando>().ReverseMap();
        }
    }
}
