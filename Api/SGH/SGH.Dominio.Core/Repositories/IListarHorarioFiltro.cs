using SGH.Dominio.Core.Enums;

namespace SGH.Dominio.Core.Repositories
{
    public class ListarHorarioFiltro
    {
        public int? CodigoCurriculo { get; set; }
        public EPeriodo? Periodo { get; set; }
        public ESemestre? Semestre { get; set; }
        public int? Ano { get; set; }
        public int codigoUsuario { get; set; }
    }
}
