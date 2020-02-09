using MediatR;
using SGH.Dominio.Core;
using SGH.Dominio.Core.Enums;
using SGH.Dominio.Core.Model;
using SGH.Dominio.ViewModel;
using System.Collections.Generic;

namespace SGH.Dominio.Implementacao.Cargos.Comandos.Criar
{
    public class CriarCargoComando : IRequest<Resposta<CriarCargoComando>>
    {
        public int? Codigo { get; set; }
        public int Numero { get; set; }
        public int Edital { get; set; }
        public int Ano { get; set; }
        public ESemestre Semestre { get; set; }
        public int? CodigoProfessor { get; set; }
        public IEnumerable<CargoDisciplinaViewModel> Disciplinas { get; set; }

    }
}
