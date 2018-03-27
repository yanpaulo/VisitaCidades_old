using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace IABusca.Mapas
{
    public class MapaGrid : Mapa
    {
        public static readonly int RandomMin = 10;
        public static readonly int RandomMax = 100;

        private int tamanho;
        private Local[,] vetor;

        public static MapaGrid Random(int tamanho = 5)
        {
            var vetor = new Local[tamanho, tamanho];
            var mapa = new MapaGrid { vetor = vetor, tamanho = tamanho };

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
                        var distancia = esquerda.Ligacoes.SingleOrDefault(l => l.Local == local)?.Distancia ?? rng.Next(RandomMin, RandomMax);

                        local.Ligacoes.Add(new Ligacao
                        {
                            Distancia = distancia,
                            Local = esquerda
                        });
                    }
                    if (i < tamanho - 1)
                    {
                        var direita = vetor[i + 1, j];
                        var distancia = direita.Ligacoes.SingleOrDefault(l => l.Local == local)?.Distancia ?? rng.Next(RandomMin, RandomMax);

                        local.Ligacoes.Add(new Ligacao
                        {
                            Distancia = distancia,
                            Local = direita
                        });
                    }
                    if (j > 0)
                    {
                        var cima = vetor[i, j - 1];
                        var distancia = cima.Ligacoes.SingleOrDefault(l => l.Local == local)?.Distancia ?? rng.Next(RandomMin, RandomMax);

                        local.Ligacoes.Add(new Ligacao
                        {
                            Distancia = distancia,
                            Local = cima
                        });
                    }
                    if (j < tamanho - 1)
                    {
                        var baixo = vetor[i, j + 1];
                        var distancia = baixo.Ligacoes.SingleOrDefault(l => l.Local == local)?.Distancia ?? rng.Next(RandomMin, RandomMax);
                        local.Ligacoes.Add(new Ligacao
                        {
                            Distancia = distancia,
                            Local = baixo
                        });
                    }
                }
            }

            return mapa;
        }

        public void Imprime()
        {
            for (int i = 0; i < tamanho; i++)
            {
                for (int j = 0; j < tamanho; j++)
                {
                    var local = vetor[i, j];
                    Console.Write(local.Nome);
                    if (j < tamanho - 1)
                    {
                        var proximo = vetor[i, j + 1];
                        var ligacao = local.Ligacoes.Single(l => l.Local == proximo);
                        Console.Write("\t");
                        ColoredPrint($"{ligacao.Distancia}");
                        Console.Write("\t");
                    }
                }
                
                Console.WriteLine();
                Console.WriteLine();
                if (i < tamanho - 1)
                {
                    for (int j = 0; j < tamanho; j++)
                    {
                        var local = vetor[i, j];
                        var proximo = vetor[i + 1, j];
                        var ligacao = local.Ligacoes.Single(l => l.Local == proximo);
                        Console.Write(" ");
                        ColoredPrint($"{ligacao.Distancia}");
                        if (i < tamanho - 1)
                        {
                            Console.Write("\t\t");
                        }
                    }
                    Console.WriteLine();
                    Console.WriteLine();
                }
            }
        }

        private static void ColoredPrint(string str)
        {
            try
            {
                Console.BackgroundColor = ConsoleColor.Cyan;
                Console.ForegroundColor = ConsoleColor.Black;
                Console.Write(str);
            }
            finally
            {
                Console.ResetColor();
            }
        }
    }
}
