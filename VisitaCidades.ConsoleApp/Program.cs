using IABusca;
using IABusca.Mapas;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace VisitaCidades.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var sw = new Stopwatch();
            sw.Start();

            var mapa = MapaGrid.Random(6);
            var problema = new Problema(new[] { 20, 10, 6 }) { Mapa = mapa };
            var busca = new BuscaHillClimbing { Problema = problema };
            var solucao = busca.Resolve();
            sw.Stop();
            if (!solucao.Valida)
            {
                Console.WriteLine("Solução ideal não encontrada. Melhor solução: ");
            }
            busca.Problema.Mapa.Imprime(solucao.Rotas);
            solucao.Imprime();
            Console.WriteLine(solucao);
            Console.WriteLine($"Tempo gasto: {sw.ElapsedMilliseconds}ms");
            if (Debugger.IsAttached)
            {
                Debugger.Break();
            }
        }
    }
}
