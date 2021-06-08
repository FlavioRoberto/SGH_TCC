using SGH.Dominio.Core.Model;

namespace SGH.Dominio.Services.Implementacao.Disciplinas.Comandos
{
    public class DisciplinaComandoBase
    {
        public int? Codigo { get; set; }
        public string Descricao { get; set; }

        public virtual Disciplina ConverterParaDisciplina()
        {
            return new Disciplina
            {
                Codigo = Codigo ?? 0,
                Descricao = Descricao
            };
        }
    }
}
