using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace IABusca
{
    public class BuscaEmProfundidadeComVisitados<T> : BuscaEmArvoreBase<T>
    {
        private Stack<No<T>> borda = new Stack<No<T>>();
        private int? limite;

        public BuscaEmProfundidadeComVisitados(IProblema<T> problema, int? limite = null) 
            : base(problema)
        {
            this.limite = limite;
            borda.Push(Raiz);
        }

        public override IEnumerable<No<T>> Borda => borda;

        public override void Expande()
        {
            var no = borda.Pop();
            Explorado.Add(no.Estado);

            if (Problema.TestaObjetivo(no.Estado))
            {
                Objetivo = no;
                return;
            }

            if (no.Profundidade >= limite)
            {
                return;
            }
            
            var resultados = Problema.Acoes(no.Estado)
                .Where(a => !Explorado.Contains(a.Resultado) && !borda.Any(b => b.Estado.Equals(a.Resultado)))
                .Select(a => a.Resultado);

            foreach (var resultado in resultados)
            {
                var filho = new No<T>
                {
                    Pai = no,
                    Estado = resultado,
                    Profundidade = no.Profundidade + 1
                };
                borda.Push(filho);

                if (Problema.TestaObjetivo(resultado))
                {
                    Objetivo = filho;
                    return;
                }
            }
        }
    }
}
