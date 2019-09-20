using Dominio.Contratos;

namespace Dominio.Model
{
    public class ProfessorCurso : EntidadeBase
    {
        public int ProfessorId { get; set; }
        public int CursoId { get; set; }

        public Professor Professor { get; set; }
        public Curso Curso { get; set; }
    }
}
