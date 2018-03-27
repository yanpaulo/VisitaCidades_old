using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace IABusca
{
    public class BuscaEmLargura<T> : BuscaEmArvoreBase<T>
    {
        private Queue<No<T>> borda = new Queue<No<T>>();

        public BuscaEmLargura(IProblema<T> problema) : 
            base(problema)
        {
            borda.Enqueue(Raiz);
        }

        public override IEnumerable<No<T>> Borda => borda;
        
        public override void Expande()
        {
            var no = borda.Dequeue();
            Explorado.Add(no.Estado);
            
            var resultados = Problema.Acoes(no.Estado)
                .Where(e => !Explorado.Contains(e.Resultado) && !borda.Any(b => b.Estado.Equals(e.Resultado)))
                .Select(a => a.Resultado);

            foreach (var resultado in resultados)
            {
                var filho = new No<T>
                {
                    Pai = no,
                    Estado = resultado,
                    Profundidade = no.Profundidade + 1
                };
                borda.Enqueue(filho);

                if (Problema.TestaObjetivo(resultado))
                {
                    Objetivo = filho;
                    return;
                }
            }
        }
    }
}
