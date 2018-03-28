using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using IABusca.Mapas;

namespace VisitaCidades
{
    public class MapaGrid : Mapa
    {
        #region Construtor Privado
        private MapaGrid() { }
        #endregion

        public int RandomMin { get; private set; }

        public int RandomMax { get; private set; }

        public int Tamanho { get; private set; }

        public Local[,] Vetor { get; private set; }

        public static MapaGrid Random(int tamanho = 5, int randomMin = 10, int randomMax = 100)
        {
            var vetor = new Local[tamanho, tamanho];
            var mapa = new MapaGrid { Vetor = vetor, Tamanho = tamanho };

            var rng = new Random();
            var nome = 1;
            for (int i = 0; i < tamanho; i++)
            {
                for (int j = 0; j < tamanho; j++)
                {
                    var local = new Local { Nome = nome++.ToString("000") };
                    vetor[i, j] = local;
                    mapa.Locais.Add(local);
                }
            }

            for (int i = 0; i < tamanho; i++)
            {
                for (int j = 0; j < tamanho; j++)
                {
                    var local = vetor[i, j];
                    if (i > 0)
                    {
                        var esquerda = vetor[i - 1, j];
                        var distancia = esquerda.Ligacoes.SingleOrDefault(l => l.Local == local)?.Distancia ?? rng.Next(randomMin, randomMax);

                        local.Ligacoes.Add(new Ligacao
                        {
                            Distancia = distancia,
                            Local = esquerda
                        });
                    }
                    if (i < tamanho - 1)
                    {
                        var direita = vetor[i + 1, j];
                        var distancia = direita.Ligacoes.SingleOrDefault(l => l.Local == local)?.Distancia ?? rng.Next(randomMin, randomMax);

                        local.Ligacoes.Add(new Ligacao
                        {
                            Distancia = distancia,
                            Local = direita
                        });
                    }
                    if (j > 0)
                    {
                        var cima = vetor[i, j - 1];
                        var distancia = cima.Ligacoes.SingleOrDefault(l => l.Local == local)?.Distancia ?? rng.Next(randomMin, randomMax);

                        local.Ligacoes.Add(new Ligacao
                        {
                            Distancia = distancia,
                            Local = cima
                        });
                    }
                    if (j < tamanho - 1)
                    {
                        var baixo = vetor[i, j + 1];
                        var distancia = baixo.Ligacoes.SingleOrDefault(l => l.Local == local)?.Distancia ?? rng.Next(randomMin, randomMax);
                        local.Ligacoes.Add(new Ligacao
                        {
                            Distancia = distancia,
                            Local = baixo
                        });
                    }
                }
            }

            mapa.RandomMin = randomMin;
            mapa.RandomMax = randomMax;

            return mapa;
        }

        public void Imprime(List<Rota> rotas)
        {
            for (int i = 0; i < Tamanho; i++)
            {
                for (int j = 0; j < Tamanho; j++)
                {
                    var local = Vetor[i, j];
                    ImprimeNomeLocal(rotas, local);

                    if (j < Tamanho - 1)
                    {
                        var proximo = Vetor[i, j + 1];
                        var ligacao = local.Ligacoes.Single(l => l.Local == proximo);
                        Console.Write("\t");
                        Util.ColoredPrint($"{ligacao.Distancia}");
                        Console.Write("\t");
                    }
                }

                Console.WriteLine();
                Console.WriteLine();
                if (i < Tamanho - 1)
                {
                    for (int j = 0; j < Tamanho; j++)
                    {
                        var local = Vetor[i, j];
                        var proximo = Vetor[i + 1, j];
                        var ligacao = local.Ligacoes.Single(l => l.Local == proximo);
                        Console.Write(" ");
                        Util.ColoredPrint($"{ligacao.Distancia}");
                        if (i < Tamanho - 1)
                        {
                            Console.Write("\t\t");
                        }
                    }
                    Console.WriteLine();
                    Console.WriteLine();
                }
            }
        }

        private static void ImprimeNomeLocal(List<Rota> rotas, Local local)
        {
            var rota = rotas.FirstOrDefault(r => r.Locais.Contains(local));
            if (rota != null)
            {
                Util.ColoredPrint(local.Nome, rota.Cor, ConsoleColor.Black);
            }
            else
            {
                Console.Write(local.Nome);
            }
        }

        
    }
}
