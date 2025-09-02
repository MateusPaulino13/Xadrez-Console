using tabuleiro;
using xadrez;

namespace Xadrez
{
    class Rei : Peca
    {
        private PartidaDeXadrez partida;

        public Rei(Cor cor, Tabuleiro tab, PartidaDeXadrez partida) : base(cor, tab)
        {
            this.partida = partida;
        }

        private bool podeMover(Posicao pos)
        {
            Peca p = Tab.peca(pos);
            return p == null || p.Cor != Cor;
        }

        private bool testeTorreParaRoque(Posicao pos)
        {
            Peca p = Tab.peca(pos);
            return p != null && p is Torre && p.Cor == Cor && p.qteMovimentos == 0;
        }

        public override bool[,] movimentosPossiveis()
        {
            bool[,] mat = new bool[Tab.linhas, Tab.colunas];
            Posicao pos = new Posicao(0, 0);

            // Acima
            pos.definirValores(Posicao.Linha - 1, Posicao.Coluna);
            if(Tab.posicaoValida(pos) && podeMover(pos))
                mat[pos.Linha, pos.Coluna] = true;

            // Abaixo
            pos.definirValores(Posicao.Linha + 1, Posicao.Coluna);
            if(Tab.posicaoValida(pos) && podeMover(pos))
                mat[pos.Linha, pos.Coluna] = true;

            // Direita
            pos.definirValores(Posicao.Linha, Posicao.Coluna + 1);
            if(Tab.posicaoValida(pos) && podeMover(pos))
                mat[pos.Linha, pos.Coluna] = true;

            // Esquerda
            pos.definirValores(Posicao.Linha, Posicao.Coluna - 1);
            if(Tab.posicaoValida(pos) && podeMover(pos))
                mat[pos.Linha, pos.Coluna] = true;

            // Nordeste
            pos.definirValores(Posicao.Linha - 1, Posicao.Coluna + 1);
            if(Tab.posicaoValida(pos) && podeMover(pos))
                mat[pos.Linha, pos.Coluna] = true;

            // Sudeste
            pos.definirValores(Posicao.Linha + 1, Posicao.Coluna + 1);
            if(Tab.posicaoValida(pos) && podeMover(pos))
                mat[pos.Linha, pos.Coluna] = true;

            // Sudoeste
            pos.definirValores(Posicao.Linha + 1, Posicao.Coluna - 1);
            if(Tab.posicaoValida(pos) && podeMover(pos))
                mat[pos.Linha, pos.Coluna] = true;

            // Noroeste
            pos.definirValores(Posicao.Linha - 1, Posicao.Coluna - 1);
            if(Tab.posicaoValida(pos) && podeMover(pos))
                mat[pos.Linha, pos.Coluna] = true;

            //jogada especial roque
            if(qteMovimentos == 0 && !partida.xeque)
            {
                //roque pequeno
                Posicao posT1 = new Posicao(Posicao.Linha, Posicao.Coluna + 3);
                if (testeTorreParaRoque(posT1))
                {
                    Posicao p1 = new Posicao(Posicao.Linha, Posicao.Coluna + 1);
                    Posicao p2 = new Posicao(Posicao.Linha, Posicao.Coluna + 2);
                    
                    if (Tab.peca(p1) == null && Tab.peca(p2) == null)
                        mat[Posicao.Linha, Posicao.Coluna + 2] = true;
                }

                //roque grande
                Posicao posT2 = new Posicao(Posicao.Linha, Posicao.Coluna - 4);
                if (testeTorreParaRoque(posT2))
                {
                    Posicao p1 = new Posicao(Posicao.Linha, Posicao.Coluna - 1);
                    Posicao p2 = new Posicao(Posicao.Linha, Posicao.Coluna - 2);
                    Posicao p3 = new Posicao(Posicao.Linha, Posicao.Coluna - 2);
                    
                    if (Tab.peca(p1) == null && Tab.peca(p2) == null && Tab.peca(p3) == null)
                        mat[Posicao.Linha, Posicao.Coluna - 2] = true;
                }
            }

            return mat;
        }

        public override string ToString()
        {
            return "R";
        }
    }
}
