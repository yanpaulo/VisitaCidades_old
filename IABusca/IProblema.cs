using System.Collections.Generic;

namespace IABusca
{
    public interface IProblema<T>
    {
        T Inicial { get; }
        
        IEnumerable<Acao<T>> Acoes(T estado);

        bool TestaObjetivo(T estado);
    }
}