using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace IABusca.Mapas
{
    public class Mapa
    {
        private Random rng = new Random();

        public List<Local> Locais { get; private set; } =
            new List<Local>();

        public static Mapa FromFile(string filename = "mapa.txt")
        {
            var mapa = new Mapa();
            var reader = new StreamReader(filename);

            string line;
            while ((line = reader.ReadLine()) != null && line.Trim() != string.Empty)
            {
                if (line.StartsWith("--"))
                {
                    continue;
                }
                var nome = line.Substring(0, line.IndexOf(":"));
                var valores = line
                    .Remove(0, line.IndexOf(":") + 1)
                    .Split(new[] { ',' })
                    .Select(p => p.Trim().Split(new[] { '-' }))
                    .Select(p => new { Nome = p[0], Distancia = int.Parse(p[1]) });

                var local = GerOrCreateLocal(mapa, nome);

                local.Ligacoes.AddRange(valores.Select(v => new Ligacao { Local = GerOrCreateLocal(mapa, v.Nome), Distancia = v.Distancia }));

            }

            while ((line = reader.ReadLine()) != null)
            {
                if (line.StartsWith("--"))
                {
                    continue;
                }
                var nome = line.Substring(0, line.IndexOf(":"));
                var valores = line
                    .Remove(0, line.IndexOf(":") + 1)
                    .Split(new[] { ',' })
                    .Select(p => p.Trim().Split(new[] { '-' }))
                    .Select(p => new { Nome = p[0], DLR = int.Parse(p[1]) });

                var local = GerOrCreateLocal(mapa, nome);

                foreach (var valor in valores)
                {
                    var destino = GerOrCreateLocal(mapa, valor.Nome);
                    local.DLR.Add(destino, valor.DLR);
                    destino.DLR[local] = valor.DLR;
                }
            }

            return mapa;
        }

        public Local LocalAleatorio() =>
            Locais[rng.Next(Locais.Count)];

        private static Local GerOrCreateLocal(Mapa mapa, string nome)
        {
            var local = mapa.Locais.SingleOrDefault(l => l.Nome == nome);
            if (local == null)
            {
                local = new Local { Nome = nome };
                mapa.Locais.Add(local);
            }

            return local;
        }
    }

    public class Local
    {
        public string Nome { get; set; }

        public List<Ligacao> Ligacoes { get; private set; }
            = new List<Ligacao>();

        public Dictionary<Local, int> DLR { get; private set; }
            = new Dictionary<Local, int>();

        public override string ToString() =>
            Nome;
    }

    public class Ligacao
    {
        public Local Local { get; set; }

        public int Distancia { get; set; }
    }
}
