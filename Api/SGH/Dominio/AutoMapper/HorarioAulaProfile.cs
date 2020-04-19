using AutoMapper;
using SGH.Dominio.Core.Model;
using SGH.Dominio.Services.Implementacao.Horarios.Comandos.Atualizar;
using SGH.Dominio.Services.Implementacao.Horarios.Comandos.Criar;
using SGH.Dominio.Services.ViewModel;

namespace SGH.Dominio.Services.AutoMapper
{
    public class HorarioAulaProfile : Profile
    {
        public HorarioAulaProfile()
        {
            CreateMap<HorarioAula, HorarioAulaViewModel>().ReverseMap();

            CreateMap<HorarioAula, CriarHorarioAulaComando>().ReverseMap();

            CreateMap<AtualizarHorarioAulaComando, HorarioAula>().ReverseMap();
        }
    }
}
