using System.Collections.Generic;

namespace SGH.Dominio.Core.Model
{
    public class Paginacao<T>
    {
        public int Quantidade { get; set; }
        public ICollection<T> Entidade { get; set; }
        public int Total { get; set; }
        public int Posicao { get; set; }
    }
}
