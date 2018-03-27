using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VisitaCidades
{
    public class BuscaHillClimbing
    {
        public int Iteracoes { get; set; } = 50;

        public Problema Problema { get; set; } =
            new Problema();

        public Solucao Resolve()
        {
            for (int i = 0; i < Iteracoes; i++)
            {
                var solucao = Problema.SolucaoAleatoria();
                var melhor = solucao;
                Console.WriteLine($"Solução Inicial: {solucao}");
                Console.WriteLine($"Custo Inicial: {solucao.CustoRota}, {solucao.CustoRepeticoes}, {solucao.CustoInicioFim}");
                while (true)
                {
                    var novaSolucao = Problema.ProximaSolucao();
                    if (novaSolucao.Custo >= solucao.Custo)
                    {
                        break;
                    }
                    solucao = novaSolucao;
                    if (solucao.Custo < melhor.Custo)
                    {
                        melhor = solucao;
                    }
                }
                Console.WriteLine($"Custo Final: {solucao.CustoRota}, {solucao.CustoRepeticoes}, {solucao.CustoInicioFim}");
                Console.WriteLine("----------------------------");
                if (solucao.Valida || i == Iteracoes - 1)
                {
                    Console.WriteLine($"Restarts: {i}");
                    return solucao;
                }

            }

            return null;
        }
    }
}
