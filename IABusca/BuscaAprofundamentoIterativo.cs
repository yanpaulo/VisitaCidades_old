using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IABusca
{
    public class BuscaAprofundamentoIterativo<T> : IAlgoritmo<T>
    {
        private IAlgoritmo<T> algoritmoProfundidade;
        
        public BuscaAprofundamentoIterativo(IProblema<T> problema)
        {
            Problema = problema;
            algoritmoProfundidade = new BuscaEmProfundidadeComVisitados<T>(problema, Limite);
        }

        public int Limite { get; private set; } = 0;

        public IProblema<T> Problema { get; private set; }

        public No<T> Objetivo =>
            algoritmoProfundidade.Objetivo;

        public IEnumerable<No<T>> Borda =>
            algoritmoProfundidade.Borda;

        public bool Falha =>
            false;

        public bool AtingiuObjetivo =>
            Objetivo != null;

        public void Expande()
        {
            if (algoritmoProfundidade.Falha)
            {
                algoritmoProfundidade = new BuscaEmProfundidadeComVisitados<T>(Problema, ++Limite);
            }
            else
            {
                algoritmoProfundidade.Expande();
            }
        }

        public string ImprimeListas() =>
            algoritmoProfundidade.ImprimeListas();
    }
}
