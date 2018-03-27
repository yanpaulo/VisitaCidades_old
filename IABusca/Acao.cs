using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IABusca
{
    public class Acao<T>
    {
        public T Resultado { get; set; }

        public int? Custo { get; set; }
    }
}
