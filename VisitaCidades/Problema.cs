using IABusca.Mapas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisitaCidades
{
    public class Problema
    {
        public MapaGrid Mapa { get; private set; }
            = MapaGrid.Random();

        public Solucao Solucao { get; private set; }

        public Solucao SolucaoAleatoria()
        {
            Solucao = Solucao.Nova(Mapa);
            return Solucao;
        }

        public Solucao ProximaSolucao()
        {
            var custo = Solucao.Custo;
            Parallel.For(0, Solucao.Lista.Count, (p, pState) =>
                Parallel.For(0, Solucao.Lista.Count, (q, qState) =>
                {
                    var lista = Solucao.Lista.ToList();
                    var item = lista[q];
                    lista.Remove(item);
                    lista.Insert(p, item);

                    var solucao = new Solucao(lista, Mapa);
                    if (solucao.Custo < custo)
                    {
                        Solucao = solucao;
                        pState.Break();
                        qState.Break();
                    }
                })
            );
            
            return Solucao;
        }
    }
}
