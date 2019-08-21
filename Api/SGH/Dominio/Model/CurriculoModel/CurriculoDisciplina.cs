using Dominio.Contratos;
using Dominio.Model.DisciplinaModel;
using System.Collections.Generic;

namespace Dominio.Model.CurriculoModel
{
    public class CurriculoDisciplina: EntidadeBase
    {
        public int CodigoDisciplina { get; set; }
        public int CodigoCurriculo { get; set; }
        public int CargaHorariaSemanalTeorica { get; set; }
        public int CargaHorariaSemanalPratica { get; set; }
        public int HoraAulaTotal { get; set; }
        public int HoraTotal { get; set; }
        public int Credito { get; set; }

        public virtual Disciplina Disciplina { get; set; }
        public virtual Curriculo Curriculo { get; set; }
        public virtual List<CurriculoDisciplinaPreRequisito> CurriculoDisciplinaPreRequisito { get; set; }

    }
}
