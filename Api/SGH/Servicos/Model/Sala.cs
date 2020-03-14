namespace SGH.Dominio.Core.Model
{
    public class Sala : EntidadeBase
    {
        public int Numero { get; set; }

        public string Descricao { get; set; }

        public bool Laboratorio { get; set; }

        public int CodigoBloco { get; set; }

        public virtual Bloco Bloco { get; set; }
    }
}
