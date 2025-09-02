using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tabuleiro;
using Xadrez;

namespace xadrez
{
    class PartidaDeXadrez
    {
        public Tabuleiro Tab { get; private set; }
        public int turno { get; private set; }
        public Cor jogadorAtual { get; private set; }
        public bool terminada { get; private set; }
        public bool xeque { get; private set; }
        public Peca vulneravelEnPassant { get; private set; }

        private HashSet<Peca> pecas;
        private HashSet<Peca> capturadas;

        public PartidaDeXadrez()
        {
            Tab = new Tabuleiro(8, 8);
            turno = 1;
            jogadorAtual = Cor.Branca;
            pecas = new HashSet<Peca>();
            capturadas = new HashSet<Peca>();
            xeque = false;
            vulneravelEnPassant = null;
            colocarPecas();
        }

        public Peca executaMovimento(Posicao origem, Posicao destino)
        {
            Peca p = Tab.retirarPeca(origem);
            p.incrementarQteMovimentos();

            Peca pecaCapturada = Tab.retirarPeca(destino);
            Tab.colocarPeca(p, destino);
            
            if (pecaCapturada != null)
                capturadas.Add(pecaCapturada);

            //jogada especial roque pequeno
            if (p is Rei && destino.Coluna == origem.Coluna + 2)
            {
                Posicao origemT = new Posicao(origem.Linha, origem.Coluna + 3);
                Posicao destinoT = new Posicao(origem.Linha, origem.Coluna + 1);
                Peca T = Tab.retirarPeca(origemT);
                T.incrementarQteMovimentos();
                Tab.colocarPeca(T, destinoT);
            }

            //jogada especial roque grande
            if (p is Rei && destino.Coluna == origem.Coluna - 2)
            {
                Posicao origemT = new Posicao(origem.Linha, origem.Coluna - 4);
                Posicao destinoT = new Posicao(origem.Linha, origem.Coluna - 1);
                Peca T = Tab.retirarPeca(origemT);
                T.incrementarQteMovimentos();
                Tab.colocarPeca(T, destinoT);
            }

            //jogada especial en passant
            if (p is Peao)
            {
                if (origem.Coluna != destino.Coluna && pecaCapturada == null)
                {
                    Posicao posP;
                    if (p.Cor == Cor.Branca)
                        posP = new Posicao(destino.Linha + 1, destino.Coluna);
                    else
                        posP = new Posicao(destino.Linha - 1, destino.Coluna);

                    pecaCapturada = Tab.retirarPeca(posP);
                    capturadas.Add(pecaCapturada);
                }
            }

            return pecaCapturada;
        }

        public void realizaJogada(Posicao origem, Posicao destino)
        {
            Peca pecaCapturada = executaMovimento(origem, destino);

            if (estaEmXeque(jogadorAtual))
            {
                desfazMovimento(origem, destino, pecaCapturada);
                throw new TabuleiroException("Você não pode se colocar em xeque!");
            }

            Peca p = Tab.peca(destino);

            // jogada especial promoção
            if (p is Peao)
            {
                if (p.Cor == Cor.Branca && destino.Linha == 0 || (p.Cor == Cor.Preta && destino.Linha == 7))
                {
                    p = Tab.retirarPeca(destino);
                    pecas.Remove(p);

                    Peca dama = new Dama(p.Cor, Tab);
                    Tab.colocarPeca(dama, destino);

                    pecas.Add(dama);
                }
            }

            if (estaEmXeque(Adversario(jogadorAtual)))
                xeque = true;
            else 
                xeque = false;

            if (testeXequeMate(Adversario(jogadorAtual)))
            {
                terminada = true;
            }
            else
            {
                turno++;
                mudaJogador();
            }

            // jogada especial en passant
            if (p is Peao && (destino.Linha == origem.Linha - 2 || destino.Linha == origem.Linha + 2))
                vulneravelEnPassant = p;
            else
                vulneravelEnPassant = null;
        }

        public void desfazMovimento(Posicao origem, Posicao destino, Peca pecaCapturada)
        {
            Peca p = Tab.retirarPeca(destino);
            p.decrementarrQteMovimentos();

            if(pecaCapturada != null)
            {
                Tab.colocarPeca(pecaCapturada, destino);
                capturadas.Remove(pecaCapturada);
            }

            //jogada especial roque pequeno
            if (p is Rei && destino.Coluna == origem.Coluna + 2)
            {
                Posicao origemT = new Posicao(origem.Linha, origem.Coluna + 3);
                Posicao destinoT = new Posicao(origem.Linha, origem.Coluna + 1);
                Peca T = Tab.retirarPeca(destinoT);
                T.decrementarrQteMovimentos();
                Tab.colocarPeca(T, origemT);
            }

            //jogada especial roque grande
            if (p is Rei && destino.Coluna == origem.Coluna - 2)
            {
                Posicao origemT = new Posicao(origem.Linha, origem.Coluna - 4);
                Posicao destinoT = new Posicao(origem.Linha, origem.Coluna - 1);
                Peca T = Tab.retirarPeca(destinoT);
                T.decrementarrQteMovimentos();
                Tab.colocarPeca(T, origemT);
            }

            //jogada especial en passant
            if (p is Peao)
            {
                if (origem.Coluna != destino.Coluna && pecaCapturada == vulneravelEnPassant)
                {
                    Peca peao = Tab.retirarPeca(destino);
                    Posicao posP;
                    if (p.Cor == Cor.Branca)
                        posP = new Posicao(3, destino.Coluna);
                    else
                        posP = new Posicao(4, destino.Coluna);
                    Tab.colocarPeca(peao, posP);
                }
            }
        }

        public void validarPosicaoDeOrigem(Posicao pos)
        {
            if (Tab.peca(pos) == null)
            {
                throw new TabuleiroException("Não existe peça na posição de origem escolhida!");
            }

            if (jogadorAtual != Tab.peca(pos).Cor)
            {
                throw new TabuleiroException("A peça de origem não é sua!");
            }

            if (!Tab.peca(pos).existeMovimentosPossiveis())
            {
                throw new TabuleiroException("Não há movimentos possíveis para a peça de origem escolhida!");
            }
        }

        public void validarPosicaoDeDestino(Posicao origem, Posicao destino)
        {
            if (!Tab.peca(origem).movimentoPossivel(destino))
                throw new TabuleiroException("Posição de destino inválida!");
        }

        private void mudaJogador()
        {
            if(jogadorAtual == Cor.Branca)
                jogadorAtual = Cor.Preta;
            else
                jogadorAtual = Cor.Branca;
        }

        public HashSet<Peca> pecasCapturadas(Cor cor)
        {
            HashSet<Peca> aux = new HashSet<Peca>();
            foreach (Peca x in capturadas)
                if (x.Cor == cor)
                    aux.Add(x);

            return aux;
        }

        public HashSet<Peca> pecasEmJogo(Cor cor)
        {
            HashSet<Peca> aux = new HashSet<Peca>();
            foreach (Peca x in pecas)
                if (x.Cor == cor)
                    aux.Add(x);
            aux.ExceptWith(pecasCapturadas(cor));
            return aux;
        }

        private Peca Rei(Cor cor)
        {
            foreach (var r in pecasEmJogo(cor))
                if (r is Rei)
                    return r;
            return null;
        }

        private Cor Adversario(Cor cor)
        {
            return cor == Cor.Branca ? Cor.Preta : Cor.Branca;
        }

        public bool testeXequeMate(Cor cor)
        {
            if(!estaEmXeque(cor))
                return false;

            foreach (var p in pecasEmJogo(cor))
            {
                bool[,] mat = p.movimentosPossiveis();
                for (int i = 0; i < Tab.linhas; i++)
                {
                    for (int j = 0; j < Tab.colunas; j++)
                    {
                        if (mat[i, j])
                        {
                            Posicao origem = p.Posicao;
                            Posicao destino = new Posicao(i, j);
                            Peca pecaCapturada = executaMovimento(origem, destino);
                            bool testeXeque = estaEmXeque(cor);
                            desfazMovimento(origem, destino, pecaCapturada);
                            if (!testeXeque)
                                return false;
                        }
                    }
                }
            }
            return true;
        }

        public bool estaEmXeque(Cor cor)
        {
            Peca rei = Rei(cor);
            if(rei == null) 
                throw new TabuleiroException($"Não tem rei da cor {cor} no tabuleiro!");

            foreach (var p in pecasEmJogo(Adversario(cor)))
            {
                bool[,] mat = p.movimentosPossiveis();
                if (mat[rei.Posicao.Linha, rei.Posicao.Coluna])
                    return true;
            }
            return false;
        }

        public void colocarNovaPeca(char coluna, int linha, Peca peca)
        {
            Tab.colocarPeca(peca, new PosicaoXadrez(coluna, linha).toPosicao());
            pecas.Add(peca);
        }

        private void colocarPecas()
        {
            colocarNovaPeca('a', 1, new Torre(Cor.Branca, Tab));
            colocarNovaPeca('b', 1, new Cavalo(Cor.Branca, Tab));
            colocarNovaPeca('c', 1, new Bispo(Cor.Branca, Tab));
            colocarNovaPeca('d', 1, new Dama(Cor.Branca, Tab));
            colocarNovaPeca('e', 1, new Rei(Cor.Branca, Tab, this));
            colocarNovaPeca('f', 1, new Bispo(Cor.Branca, Tab));
            colocarNovaPeca('g', 1, new Cavalo(Cor.Branca, Tab));
            colocarNovaPeca('h', 1, new Torre(Cor.Branca, Tab));
            colocarNovaPeca('a', 2, new Peao(Cor.Branca, Tab, this));
            colocarNovaPeca('b', 2, new Peao(Cor.Branca, Tab, this));
            colocarNovaPeca('c', 2, new Peao(Cor.Branca, Tab, this));
            colocarNovaPeca('d', 2, new Peao(Cor.Branca, Tab, this));
            colocarNovaPeca('e', 2, new Peao(Cor.Branca, Tab, this));
            colocarNovaPeca('f', 2, new Peao(Cor.Branca, Tab, this));
            colocarNovaPeca('g', 2, new Peao(Cor.Branca, Tab, this));
            colocarNovaPeca('h', 2, new Peao(Cor.Branca, Tab, this));

            colocarNovaPeca('a', 8, new Torre(Cor.Preta, Tab));
            colocarNovaPeca('b', 8, new Cavalo(Cor.Preta, Tab));
            colocarNovaPeca('c', 8, new Bispo(Cor.Preta, Tab));
            colocarNovaPeca('d', 8, new Dama(Cor.Preta, Tab));
            colocarNovaPeca('e', 8, new Rei(Cor.Preta, Tab, this));
            colocarNovaPeca('f', 8, new Bispo(Cor.Preta, Tab));
            colocarNovaPeca('g', 8, new Cavalo(Cor.Preta, Tab));
            colocarNovaPeca('h', 8, new Torre(Cor.Preta, Tab));
            colocarNovaPeca('a', 7, new Peao(Cor.Preta, Tab, this));
            colocarNovaPeca('b', 7, new Peao(Cor.Preta, Tab, this));
            colocarNovaPeca('c', 7, new Peao(Cor.Preta, Tab, this));
            colocarNovaPeca('d', 7, new Peao(Cor.Preta, Tab, this));
            colocarNovaPeca('e', 7, new Peao(Cor.Preta, Tab, this));
            colocarNovaPeca('f', 7, new Peao(Cor.Preta, Tab, this));
            colocarNovaPeca('g', 7, new Peao(Cor.Preta, Tab, this));
            colocarNovaPeca('h', 7, new Peao(Cor.Preta, Tab, this));
        }
    }
}
