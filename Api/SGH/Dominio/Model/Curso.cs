using Dominio.Contratos;
using System.Collections.Generic;

namespace Dominio.Model
{
    public class Curso : EntidadeBase
    {
        public string Descricao { get; set; }

        public virtual List<Curriculo> Curriculos { get; set; }
        public virtual List<ProfessorCurso> Professores { get; set; }
    }
}
