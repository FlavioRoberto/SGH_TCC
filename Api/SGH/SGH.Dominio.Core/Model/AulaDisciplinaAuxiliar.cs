namespace SGH.Dominio.Core.Model
{
    public class AulaDisciplinaAuxiliar : EntidadeBase
    {
        public long CodigoAula { get; private set; }
        public long CodigoCargoDisciplina { get; private set; }
        public virtual CargoDisciplina Disciplina { get; private set; }
        public virtual Aula Aula { get; private set; }

        protected AulaDisciplinaAuxiliar()
        {
        }

        public AulaDisciplinaAuxiliar(long codigoDisciplina, long codigoAula)
        {
            this.CodigoCargoDisciplina = codigoDisciplina;
            this.CodigoAula = codigoAula;
        }

        public void AdicionarDisciplina(CargoDisciplina cargoDisciplina)
        {
            this.Disciplina = cargoDisciplina;
        }

    }
}
