using MediatR;
using SGH.Dominio.Core;
using SGH.Dominio.Services.Implementacao.Aulas.ViewModels;
using System.Collections.Generic;

namespace SGH.Dominio.Services.Implementacao.Aulas.Consulta.ListarPorHorario
{
    public class ListarAulaPorHorarioConsulta : IRequest<Resposta<ICollection<AulaViewModel>>>
    {
        public int CodigoHorario { get; set; }
    }
}
