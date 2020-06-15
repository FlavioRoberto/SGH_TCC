using SGH.Dominio.Services.ViewModel;
using SGH.Dominio.ViewModel;
using System.Collections.Generic;

namespace SGH.Dominio.Services.Implementacao.CurriculosDisciplinas.Comandos.Base
{
    public class CurriculoDisciplinaComandoBase
    {
        public int Periodo { get; set; }

        public int CodigoDisciplina { get; set; }

        public int CodigoCurriculo { get; set; }

        public int AulasSemanaisTeorica { get; set; }

        public int AulasSemanaisPratica { get; set; }

        public int QuantidadeAulaTotal { get; set; }


        public IEnumerable<DisciplinCurriculoPreRequisitoaViewModel> PreRequisitos { get; set; }
    }
}
