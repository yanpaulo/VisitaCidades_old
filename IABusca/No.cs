using System;

namespace IABusca
{

    public class No<T>
    {
        public No<T> Pai { get; set; }

        public T Estado { get; set; }

        public int Profundidade { get; set; }

        public int? Custo { get; set; }
    }
}
