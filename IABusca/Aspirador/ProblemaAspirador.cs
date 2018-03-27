using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IABusca.Aspirador
{
    public class ProblemaAspirador : IProblema<Ambiente>
    {
        public ProblemaAspirador(Ambiente ambiente)
        {
            Inicial = ambiente;
        }
        public Ambiente Inicial { get; private set; }

        public bool TestaObjetivo(Ambiente estado) =>
            estado.Posicoes.All(p => p == EstadoPosicao.Limpo);

        public IEnumerable<Acao<Ambiente>> Acoes(Ambiente estado)
        {
            yield return new Acao<Ambiente> { Resultado = new Ambiente(estado, true) };

            yield return new Acao<Ambiente>
            {
                Resultado = new Ambiente(estado)
                {
                    PosicaoAspirador = estado.PosicaoAspirador > 0 ?
                                        estado.PosicaoAspirador - 1 : estado.PosicaoAspirador
                }
            };

            yield return new Acao<Ambiente>
            {
                Resultado = new Ambiente(estado)
                {
                    PosicaoAspirador = estado.PosicaoAspirador < estado.Posicoes.Count() - 1 ?
                                        estado.PosicaoAspirador + 1 : estado.PosicaoAspirador
                }
            };
             
        }

    }
}
