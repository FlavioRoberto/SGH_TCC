using SGH.Dominio.Core.Enums;
using System.Collections.Generic;

namespace SGH.Dominio.Core.Model
{
    public class Horario : EntidadeBase
    {
        public int Ano { get; set; }
        public string Descricao { get; set; }
        public ESemestre Semestre { get; set; }
        public EPeriodo Periodo { get; set; }
        public long CodigoTurno { get; set; }
        public long CodigoCurriculo { get; set; }
        public string Mensagem { get; set; }
        public virtual Turno Turno { get; set; }
        public virtual Curriculo Curriculo { get; set; }
        public virtual ICollection<Aula> Aulas { get; set; }
    }
}
