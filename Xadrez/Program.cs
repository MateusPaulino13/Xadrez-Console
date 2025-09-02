using tabuleiro;

namespace Xadrez
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Tabuleiro tab = new Tabuleiro(8, 8);

                tab.colocarPeca(new Torre(Cor.Preta, tab), new Posicao(0, 0));
                tab.colocarPeca(new Torre(Cor.Preta, tab), new Posicao(1, 3));
                tab.colocarPeca(new Rei(Cor.Preta, tab), new Posicao(2, 4));

                tab.colocarPeca(new Rei(Cor.Branca, tab), new Posicao(1, 0));
                tab.colocarPeca(new Torre(Cor.Branca, tab), new Posicao(4, 2));
                tab.colocarPeca(new Torre(Cor.Branca, tab), new Posicao(6, 3));

                Tela.ImprimirTabuleiro(tab);
            }
            catch (TabuleiroException e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
