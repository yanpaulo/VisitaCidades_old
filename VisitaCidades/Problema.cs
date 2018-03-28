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
        public Problema(int[] tamanhoRotas)
        {
            if (tamanhoRotas.Count() != 3)
            {
                throw new InvalidOperationException("Exatamente 3 tamanhos de rota devem ser especificados.");
            }
            if (tamanhoRotas.Sum() < Mapa.Locais.Count)
            {
                throw new InvalidOperationException("Menos locais a visitar que o necessário.");
            }

            Rotas = new List<Rota>
            {
                new Rota
                {
                    Nome = "Jonas",
                    Cor = ConsoleColor.DarkRed,
                    Tamanho = tamanhoRotas[0]
                },
                new Rota
                {
                    Nome = "Raquel",
                    Cor = ConsoleColor.DarkBlue,
                    Tamanho = tamanhoRotas[1]
                },
                new Rota
                {
                    Nome = "Alucard",
                    Cor = ConsoleColor.DarkGreen,
                    Tamanho = tamanhoRotas[2]
                },
            };
        }


        public MapaGrid Mapa { get; set; }
            = MapaGrid.Random();

        public List<Rota> Rotas { get; private set; }

        public Solucao Solucao { get; private set; }

        public Solucao SolucaoAleatoria() => 
            Solucao = Solucao.Nova(Mapa, Rotas);
        

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

                    var solucao = Solucao.Nova(Mapa, lista, Rotas);
                    if (solucao.Custo < custo)
                    {
                        Solucao = solucao;
                        pState.Break();
                        qState.Break();
                    }
                })
            );

            Solucao.AtualizaRotas();
            return Solucao;
        }
    }
}
