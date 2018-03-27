using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace IABusca
{
    public abstract class BuscaEmArvoreBase<T> : IAlgoritmo<T>
    {
        public BuscaEmArvoreBase(IProblema<T> problema)
        {
            Problema = problema;
            Raiz = new No<T>
            {
                Estado = Problema.Inicial
            };
        }

        public IProblema<T> Problema { get; private set; }

        public No<T> Raiz { get; protected set; }

        public No<T> Objetivo { get; protected set; }

        public IList<T> Explorado { get; private set; } = new List<T>();

        public abstract IEnumerable<No<T>> Borda { get; }

        public bool AtingiuObjetivo => Objetivo != null;

        public bool Falha => !Borda.Any();

        public abstract void Expande();

        public string ImprimeListas()
        {
            var str = $@"
Explorado:  [{ string.Join(", ", Explorado.Select(l => l.ToString())) }]
Borda:      [{ string.Join(", ", Borda.Select(b => b.Estado.ToString())) }]";

            return str;
        }
    }
}
