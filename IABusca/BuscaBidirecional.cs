using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using IABusca.Mapas;

namespace IABusca
{
    public enum TipoAlgoritmo
    {
        BuscaEmLargura,
        BuscaEmProfundidade,
        
    }
    public class BuscaBidirecional : IAlgoritmo<Local>
    {
        private IAlgoritmo<Local> a1, a2;
        private No<Local> objetivo;

        public BuscaBidirecional(ProblemaMapa problema, TipoAlgoritmo a1 = TipoAlgoritmo.BuscaEmLargura, TipoAlgoritmo a2 = TipoAlgoritmo.BuscaEmLargura)
        {
            Problema = problema;
            var inverso = new ProblemaMapa
            {
                Mapa = problema.Mapa,
                Inicial = problema.Destino,
                Destino = problema.Inicial
            };
            
            this.a1 = a1 == TipoAlgoritmo.BuscaEmLargura ? 
                new BuscaEmLargura<Local>(problema) as IAlgoritmo<Local> : 
                new BuscaEmProfundidadeComVisitados<Local>(problema);

            this.a2 = a2 == TipoAlgoritmo.BuscaEmProfundidade ?
                new BuscaEmLargura<Local>(inverso) as IAlgoritmo<Local> :
                new BuscaEmProfundidadeComVisitados<Local>(inverso);


        }
        
        public IEnumerable<No<Local>> Borda => 
            new[] { a1.Borda, a2.Borda }
            .SelectMany(a => a)
            .ToList();

        public No<Local> Objetivo => objetivo ?? a1.Objetivo ?? a2.Objetivo;

        public bool Falha => a1.Falha && a2.Falha;

        public bool AtingiuObjetivo => Objetivo != null;

        public IProblema<Local> Problema { get; private set; }

        public void Expande()
        {
            if (AtingiuObjetivo || Falha)
            {
                throw new InvalidOperationException("Não é possível expandir no estado atual.");
            }

            if (!a1.Falha)
            {
                a1.Expande();
            }
            if (AtingiuObjetivo)
            {
                return;
            }

            BuscaObjetivo();
            if (AtingiuObjetivo)
            {
                return;
            }

            if (!a2.Falha)
            {
                a2.Expande();
            }
            if (AtingiuObjetivo)
            {
                return;
            }

            BuscaObjetivo();
        }

        public string ImprimeListas()
        {
            return $@"
a1:
{a1.ImprimeListas()}

a2:
{a2.ImprimeListas()}
";
        }

        private void BuscaObjetivo()
        {
            No<Local> no2 = null;
            var no1 = a1.Borda.FirstOrDefault(n1 => a2.Borda.Any(n2 => (no2 = n2).Estado == n1.Estado));

            //Se no1 != null, significa que ele "tocou" no2.
            if (no1 != null)
            {
                //Inverte a ordem do caminho, trocando os pais a partir de no2.
                var anterior = no1;
                var atual = no2.Pai;
                var proximo = atual.Pai;
                
                while (atual != null)
                {
                    proximo = atual.Pai;
                    atual.Pai = anterior;
                    anterior = atual;
                    atual = proximo;
                }

                objetivo = anterior;
            }
        }
    }
}
