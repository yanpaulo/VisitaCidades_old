using IABusca.Mapas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IABusca
{
    public class BuscaGulosa<T> : IAlgoritmo<T>
    {
        private List<No<T>> borda = new List<No<T>>();
        private IProblemaHeuristica<T> problema;
        public BuscaGulosa(IProblemaHeuristica<T> problema)
        {
            this.problema = problema;
            borda.Add(new No<T> { Estado = problema.Inicial });
        }

        public IProblema<T> Problema => problema;

        public No<T> Objetivo { get; private set; }

        public IEnumerable<No<T>> Borda => borda;

        public bool Falha => !Borda.Any();

        public bool AtingiuObjetivo => Objetivo != null;

        public void Expande()
        {
            var pai = borda.OrderBy(n => problema.ValorHeuristica(n.Estado)).First();
            borda.Remove(pai);

            var acoes = Problema.Acoes(pai.Estado).Where(a => !Borda.Any(n => a.Resultado.Equals(n.Estado)));

            foreach (var acao in acoes)
            {
                var no = new No<T>
                {
                    Estado = acao.Resultado,
                    Pai = pai,
                };

                borda.Add(no);

                if (Problema.TestaObjetivo(no.Estado))
                {
                    Objetivo = no;
                    return;
                }
            }
        }

        public string ImprimeListas()
        {
            var str = $@"
Borda:      [{ string.Join(", ", Borda.Select(b => b.Estado.ToString())) }]";
            return str;
        }
    }
}
