using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VisitaCidades
{
    public class BuscaHillClimbing
    {
        private readonly int iteracoes = 100;

        public Problema Problema { get; private set; } =
            new Problema();

        public Solucao Resolve()
        {
            for (int i = 0; i < iteracoes; i++)
            {
                var solucao = Problema.SolucaoAleatoria();
                Console.WriteLine("Solução Inicial: " + string.Join(", ", solucao.Lista.Select(l => l.Nome)));
                Console.WriteLine($"Custo Inicial: {solucao.CustoRota}, {solucao.CustoRepeticoes}, {solucao.CustoInicioFim}");
                while (true)
                {
                    var novaSolucao = Problema.ProximaSolucao();
                    if (novaSolucao.Custo >= solucao.Custo)
                    {
                        break;
                    }
                    solucao = novaSolucao;
                }
                Console.WriteLine($"Custo Final: {solucao.CustoRota}, {solucao.CustoRepeticoes}, {solucao.CustoInicioFim}");
                Console.WriteLine("----------------------------");
                if (solucao.Valida || i == iteracoes - 1)
                {
                    Console.WriteLine($"Restarts: {i}");
                    return solucao;
                }

            }

            return null;
        }
    }
}
