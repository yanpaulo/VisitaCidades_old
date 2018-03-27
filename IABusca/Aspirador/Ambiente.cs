using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IABusca.Aspirador
{
    public enum EstadoPosicao
    {
        Sujo, Limpo
    };

    public class Ambiente
    {
        public Ambiente(int tamanho = 2, int posicaoAspirador = 0)
        {
            Posicoes = Enumerable.Range(0, tamanho).Select(i => EstadoPosicao.Sujo).ToArray();
            PosicaoAspirador = posicaoAspirador;
        }

        public Ambiente(Ambiente original, bool limpaPosicao = false)
        {
            Posicoes = original.Posicoes.ToArray();
            PosicaoAspirador = original.PosicaoAspirador;
            if (limpaPosicao)
            {
                Posicoes[PosicaoAspirador] = EstadoPosicao.Limpo;
            }
        }

        public EstadoPosicao[] Posicoes { get; private set; }

        public int PosicaoAspirador { get; set; }
        
        public override bool Equals(object obj)
        {
            var ambiente = obj as Ambiente;
            if (ambiente == null || ambiente.Posicoes.Length != Posicoes.Length || ambiente.PosicaoAspirador != PosicaoAspirador)
            {
                return false;
            }
            for (int i = 0; i < Posicoes.Length; i++)
            {
                if (Posicoes[i] != ambiente.Posicoes[i])
                {
                    return false;
                }
            }

            return true;
        }

        public override int GetHashCode()
        {
            var hashCode = 818108862;
            hashCode = hashCode * -1521134295 + EqualityComparer<EstadoPosicao[]>.Default.GetHashCode(Posicoes);
            hashCode = hashCode * -1521134295 + PosicaoAspirador.GetHashCode();
            return hashCode;
        }

        public override string ToString() =>
            $@"{{{string.Join(", "
                , Enumerable.Range(0, Posicoes.Count())
                    .Select(p => $"{p}{(p == PosicaoAspirador ? "*" : "")}: {Posicoes[p]}"))}}}";
    }
}
