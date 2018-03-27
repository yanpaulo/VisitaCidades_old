using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IABusca
{
    public class BuscaCustoUniforme<T> : BuscaEmArvoreBase<T>
    {
        private List<No<T>> borda = new List<No<T>>();

        public BuscaCustoUniforme(IProblema<T> problema) : base(problema)
        {
            Raiz.Custo = 0;
            borda.Add(Raiz);
        }

        public override IEnumerable<No<T>> Borda => borda;

        public override void Expande()
        {
            var pai = borda.OrderBy(n => n.Custo).FirstOrDefault();
            if (Problema.TestaObjetivo(pai.Estado))
            {
                Objetivo = pai;
                return;
            }

            borda.Remove(pai);
            Explorado.Add(pai.Estado);

            var acoes = Problema.Acoes(pai.Estado);

            foreach (var a in acoes)
            {
                var no = new No<T>
                {
                    Pai = pai,
                    Estado = a.Resultado,
                    Custo = pai.Custo + (a.Custo ?? throw new InvalidOperationException("Foi encontrada uma ação com custo nulo. Impossível continuar."))
                };
                if (!Explorado.Contains(no.Estado) && !Borda.Any(n => n.Estado.Equals(no.Estado)))
                {
                    borda.Add(no);
                }
                else
                {
                    var old = borda.SingleOrDefault(n => n.Estado.Equals(no.Estado));
                    if (no.Custo < old?.Custo)
                    {
                        borda.Remove(old);
                        borda.Add(no);
                    }
                }
            }

            
        }
    }
}
