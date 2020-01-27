using SGH.Dominio.Core.Model;
using System.Collections.Generic;

namespace SGH.Dominio.Implementacao.Curriculos.Comandos
{
    public class CurriculoComandoBase
    {
        public int? Codigo { get; set; }
        public int CodigoCurso { get; set; }
        public int Ano { get; set; }
        public virtual Curso Curso { get; set; }
        public virtual IEnumerable<CurriculoDisciplina> Disciplinas { get; set; }
        public virtual Curriculo ConverterParaCurriculo()
        {
            var curriculo = new Curriculo
            {
                Ano = Ano,
                CodigoCurso = CodigoCurso,
                Curso = Curso,
                Disciplinas = Disciplinas
            };

            if (Codigo.HasValue)
                curriculo.Codigo = Codigo.Value;

            return curriculo;
        }
    }
}
