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
            var mapa = MapaGrid.Random(5);
            var problema = new Problema { Mapa = mapa };
            var busca = new BuscaHillClimbing { Problema = problema };
            var solucao = busca.Resolve();

            if (!solucao.Valida)
            {
                Console.WriteLine("Solução ideal não encontrada. Melhor solução: ");
            }
            busca.Problema.Mapa.Imprime();
            Console.WriteLine(solucao);

            if (Debugger.IsAttached)
            {
                Debugger.Break();
            }
        }
    }
}
