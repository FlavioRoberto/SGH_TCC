namespace SGH.Dominio.Services.Implementacao.Salas.Comandos.Base
{
    public abstract class SalaComando
    {
        public int Numero { get; set; }

        public string Descricao { get; set; }

        public bool? Laboratorio { get; set; }

        public int CodigoBloco { get; set; }
    }
}
