using MediatR;
using SGH.Dominio.Core;
using SGH.Dominio.Core.Enums;
using SGH.Dominio.ViewModel;
using System.Collections.Generic;

namespace SGH.Dominio.Implementacao.Cargos.Comandos.Atualizar
{
    public class AtualizarCargoComando : IRequest<Resposta<CargoViewModel>>
    {
        public int? Codigo { get; set; }
        public int Numero { get; set; }
        public int Edital { get; set; }
        public int Ano { get; set; }
        public ESemestre Semestre { get; set; }
        public int? CodigoProfessor { get; set; }
        public List<CargoDisciplinaViewModel> Disciplinas { get; set; }
    }
}
