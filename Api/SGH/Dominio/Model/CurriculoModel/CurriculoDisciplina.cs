using Dominio.Model.DisciplinaModel;
using System.Collections.Generic;

namespace Dominio.Model.CurriculoModel
{
    public class CurriculoDisciplina
    {
        public int Codigo { get; set; }
        public int CodigoDisciplina { get; set; }
        public int CodigoCurriculo { get; set; }
        public int CargaHorariaSemanalTeorica { get; set; }
        public int CargaHorariaSemanalPratica { get; set; }
        public int CargaHorariaSemanalTotal { get; set; }
        public int HoraAulaTotal { get; set; }
        public int HoraTotal { get; set; }
        public int Credito { get; set; }
        public bool PreRequisito { get; set; }

        public virtual Disciplina Disciplina { get; set; }
        public virtual Curriculo Curriculo { get; set; }
    }
}
