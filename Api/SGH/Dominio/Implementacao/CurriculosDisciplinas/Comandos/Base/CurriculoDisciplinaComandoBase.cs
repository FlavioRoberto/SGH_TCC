﻿using SGH.Dominio.ViewModel;
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

        public IEnumerable<DisciplinaViewModel> PreRequisitos { get; set; }
    }
}
