using System;

namespace Dominio.ViewModel
{
    public class Paginacao<T>
    {
        public T Entidade { get; set; }
        public int Total { get; set; }
        public int Posicao { get; set; }
    }
}
