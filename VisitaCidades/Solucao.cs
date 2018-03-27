using IABusca;
using IABusca.Mapas;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Diagnostics;

namespace VisitaCidades
{
    public class Solucao
    {
        public List<Local> Lista { get; private set; }
        public int Custo { get; private set; }
        public bool Valida { get; private set; }
        public Mapa Mapa { get; private set; }

        public int CustoRota { get; private set; }
        public int CustoRepeticoes { get; private set; }
        public int CustoInicioFim { get; private set; }

        public Solucao(List<Local> lista, Mapa mapa)
        {
            Mapa = mapa;
            Lista = lista.ToList();

            CalculaCusto();
            ValidaSolucao();
        }

        public static Solucao Random(MapaGrid mapa)
        {
            var rng = new Random();
            var lista = new List<Local>();
            var atual = mapa.LocalAleatorio();

            while (lista.Count < mapa.Locais.Count)
            {
                var locais = mapa.Locais.Where(l => !lista.Contains(l)).ToList();
                lista.Add(locais[rng.Next(locais.Count)]);
            }

            return new Solucao(lista, mapa);
        }

        public static Solucao Nova(MapaGrid mapa)
        {
            var rng = new Random();
            var lista = new List<Local>();
            var atual = mapa.LocalAleatorio();

            lista.Add(atual);

            while (lista.Count < mapa.Locais.Count)
            {
                Local proximo =
                    atual.Ligacoes.Where(l => !lista.Contains(l.Local)).FirstOrDefault()?.Local ??
                    mapa.Locais.Where(l => !lista.Contains(l)).First();

                lista.Add(proximo);
                atual = proximo;
            }

            return new Solucao(lista, mapa);
        }

        private void ValidaSolucao()
        {
            Valida = CustoRepeticoes == 0;
        }

        private void CalculaCusto()
        {
            int custo = 0;

            for (int i = 0; i < Lista.Count - 1; i++)
            {
                var bcu = ResolveBCU(Lista[i], Lista[i + 1]);
                var solucao = bcu.Solucao();

                CustoRota += bcu.Objetivo.Custo.Value;
                CustoRepeticoes += solucao.Count() - 2;
            }
            CustoRepeticoes *= 100;

            custo += CustoRota;
            custo += CustoRepeticoes;

            custo += (CustoInicioFim = ResolveBCU(Lista.First(), Lista.Last()).Objetivo.Custo.Value * 2);

            Custo = custo;
        }

        private BuscaCustoUniforme<Local> ResolveBCU(Local origem, Local destino)
        {
            var problemaMapa = new ProblemaMapa
            {
                Inicial = origem,
                Destino = destino,
                Mapa = Mapa
            };
            var bcu = new BuscaCustoUniforme<Local>(problemaMapa);
            while (!bcu.AtingiuObjetivo && !bcu.Falha)
            {
                bcu.Expande();
            }
            if (bcu.Falha)
            {
                throw new InvalidOperationException("Sub-problema não solucionável por BCU.");
            }

            return bcu;

        }

    }
}
