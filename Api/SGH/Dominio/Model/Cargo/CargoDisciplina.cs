using Dominio.Model.CurriculoModel;

namespace Dominio.Model
{
    public class CargoDisciplina
    {
        public int CodigoCurriculoDisciplina { get; set; }
        public int CodigoCargo { get; set; }       

        public virtual CurriculoDisciplina CurriculoDisciplina { get; set; }
        public virtual Cargo Cargo { get; set; } 
    }
}