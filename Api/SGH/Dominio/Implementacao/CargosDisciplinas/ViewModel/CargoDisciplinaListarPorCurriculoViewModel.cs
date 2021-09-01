using AutoMapper;

namespace SGH.Dominio.Services.Implementacao.CargosDisciplinas.ViewModel
{
    public class CargoDisciplinaListarPorCurriculoViewModel 
    {
        public int? Codigo { get; set; }
        public long CodigoCurriculoDisciplina { get; set; }
        public string Descricao { get; set; }
        public string Professor { get; set; }
    }
}
