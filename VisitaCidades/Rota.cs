using IABusca.Mapas;
using System;
using System.Collections.Generic;
using System.Text;

namespace VisitaCidades
{
    public class Rota
    {
        public string Nome { get; set; }

        public IEnumerable<Local> Locais { get; set; }

        public ConsoleColor Cor { get; set; }

        public int Tamanho { get; set; }

        public Rota AsCopy() => new Rota
        {
            Nome = Nome,
            Locais = Locais,
            Cor = Cor,
            Tamanho = Tamanho
        };
    }
}
