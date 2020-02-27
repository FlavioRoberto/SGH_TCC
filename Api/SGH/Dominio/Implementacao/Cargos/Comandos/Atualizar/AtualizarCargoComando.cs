using MediatR;
using SGH.Dominio.Core;
using SGH.Dominio.Core.Enums;
using SGH.Dominio.Services.Implementacao.Cargos.Comandos.Base;
using SGH.Dominio.ViewModel;

namespace SGH.Dominio.Services.Implementacao.Cargos.Comandos.Atualizar
{
    public class AtualizarCargoComando : IRequest<Resposta<CargoViewModel>>, ICargoComando
    {
        public int? Codigo { get; set; }
        public int Numero { get; set; }
        public int Edital { get; set; }
        public int Ano { get; set; }
        public ESemestre Semestre { get; set; }
        public int? CodigoProfessor { get; set; }
    }
}
