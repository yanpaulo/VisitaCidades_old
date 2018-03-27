using IABusca;
using IABusca.Mapas;
using System;
using System.Diagnostics;
using System.Linq;

namespace VisitaCidades.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var bhc = new BuscaHillClimbing();
            var solucao = bhc.Resolve();

            bhc.Problema.Mapa.Imprime();
            Console.WriteLine(string.Join(", ", solucao.Lista.Select(l => l.Nome)));
            if (!solucao.Valida)
            {
                Console.WriteLine("Solução Inválida!!!!!!");
            }
            if (Debugger.IsAttached)
            {
                Debugger.Break();
            }
        }
    }
}
