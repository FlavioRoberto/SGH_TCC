using SGH.Dominio.Core.Enums;

namespace SGH.Dominio.Services.Implementacao.Cargos.Comandos.Base
{
    public interface ICargoComando
    {
        int? Codigo { get; set; }
        int Numero { get; set; }
        int Edital { get; set; }
        int Ano { get; set; }
        ESemestre Semestre { get; set; }
        int? CodigoProfessor { get; set; }
    }
}
