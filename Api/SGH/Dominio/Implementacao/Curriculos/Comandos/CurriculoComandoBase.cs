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
            return new Curriculo
            {
                Ano = Ano,
                Codigo = Codigo.Value,
                CodigoCurso = CodigoCurso,
                Curso = Curso,
                Disciplinas = Disciplinas
            };
        }
    }
}
