using SGH.Dominio.Core.Model;
using SGH.Dominio.ViewModel;
using System.Collections.Generic;

namespace SGH.Dominio.Services.Implementacao.Curriculos.Comandos
{
    public class CurriculoComandoBase
    {
        public int? Codigo { get; set; }
        public int CodigoCurso { get; set; }
        public int Ano { get; set; }
        public virtual CursoViewModel Curso { get; set; }
    }
}
