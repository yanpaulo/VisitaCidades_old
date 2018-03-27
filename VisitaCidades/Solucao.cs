﻿using IABusca;
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

        public MapaGrid Mapa { get; private set; }
        public List<Local> Lista { get; private set; }
        public int Custo { get; private set; }

        public int CustoRota { get; private set; }
        public int CustoRepeticoes { get; private set; }
        public int CustoInicioFim { get; private set; }

        public bool Valida => CustoRepeticoes == 0;

        #region Construtor Privado
        private Solucao() { } 
        #endregion

        public Solucao(List<Local> lista, MapaGrid mapa)
        {
            Mapa = mapa;
            Lista = lista.ToList();

            CalculaCusto();
        }

        public static Solucao Nova(MapaGrid mapa, List<Local> lista)
        {
            var solucao =
                new Solucao
                {
                    Mapa = mapa,
                    Lista = lista.ToList()
                };

            solucao.CalculaCusto();

            return solucao;
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

        private void CalculaCusto()
        {
            int custo = 0;

            for (int i = 0; i < Lista.Count - 1; i++)
            {
                var bcu = BCU(Lista[i], Lista[i + 1]);
                var solucao = bcu.Solucao();

                CustoRota += bcu.Objetivo.Custo.Value;
                CustoRepeticoes += solucao.Count() > 2 ? bcu.Objetivo.Custo.Value * 2 : 0;
            }

            custo += CustoRota;
            custo += CustoRepeticoes;

            custo += (CustoInicioFim = BCU(Lista.First(), Lista.Last()).Objetivo.Custo.Value);

            Custo = custo;
        }

        private BuscaCustoUniforme<Local> BCU(Local origem, Local destino)
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



        public override string ToString() =>
            string.Join(", ", Lista.Select(l => l.Nome));
    }
}
