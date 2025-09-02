using tabuleiro;
using xadrez;

namespace Xadrez
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                PartidaDeXadrez partida = new PartidaDeXadrez();
                while (!partida.terminada)
                {
                    Console.Clear();
                    Tela.ImprimirTabuleiro(partida.tab);

                    Console.WriteLine();
                    Console.WriteLine($"Turno : {partida.turno}");
                    Console.WriteLine($"Aguardando jogada : {partida.jogadorAtual}");

                    Console.WriteLine();

                    Console.Write("Origem : ");
                    Posicao origem = Tela.lerPosicaoXadrez().toPosicao();
                    bool[,] posicoesPossiveis = partida.tab.peca(origem).movimentosPossiveis();


                    Console.Clear();
                    Tela.ImprimirTabuleiro(partida.tab, posicoesPossiveis);

                    Console.WriteLine();

                    Console.Write("Destino : ");
                    Posicao destno = Tela.lerPosicaoXadrez().toPosicao();

                    partida.realizaJogada(origem, destno);
                }
            }
            catch (TabuleiroException e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
