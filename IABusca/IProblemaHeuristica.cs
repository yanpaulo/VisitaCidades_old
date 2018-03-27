using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IABusca
{
    public interface IProblemaHeuristica<T> : IProblema<T>
    {
        int ValorHeuristica(T estado);
    }
}
