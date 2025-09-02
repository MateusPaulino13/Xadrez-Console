# Xadrez Console

Bem-vindo ao **Xadrez Console**! Este é um projeto de implementação do jogo de xadrez totalmente jogável via terminal/console em C#. O objetivo é proporcionar uma experiência divertida, educativa e desafiadora para quem deseja jogar xadrez no console, além de servir como estudo de lógica de programação, orientação a objetos e manipulação de entrada/saída em C#.

---

## Índice

- [Sobre o Projeto](#sobre-o-projeto)
- [Funcionalidades](#funcionalidades)
- [Demonstração](#demonstração)
- [Como Usar](#como-usar)
- [Requisitos](#requisitos)
- [Estrutura do Projeto](#estrutura-do-projeto)
- [Possíveis Melhorias](#possíveis-melhorias)
- [Contribuições](#contribuições)
- [Licença](#licença)
- [Autor](#autor)

---

## Sobre o Projeto

O **Xadrez Console** foi desenvolvido com o intuito de praticar conceitos de programação orientada a objetos, simular partidas de xadrez reais e permitir que qualquer pessoa possa jogar xadrez diretamente no terminal, sem necessidade de interface gráfica.

Este projeto pode ser utilizado tanto para diversão quanto para estudo, servindo como base para implementações mais avançadas de jogos de tabuleiro em C#.

---

## Funcionalidades

- Tabuleiro de xadrez renderizado no console
- Movimentação de todas as peças conforme as regras oficiais
- Detecção de xeque e xeque-mate
- Indicação de peças capturadas
- Alternância correta entre jogadores (brancas e pretas)
- Validação de movimentos legais e ilegais
- Suporte a promoção de peão
- Mensagens de erro e ajuda no console
- Partida termina quando ocorrer xeque-mate ou empate

---

## Demonstração

```bash
Posição inicial:
8 r n b q k b n r
7 p p p p p p p p
6 . . . . . . . .
5 . . . . . . . .
4 . . . . . . . .
3 . . . . . . . .
2 P P P P P P P P
1 R N B Q K B N R
  a b c d e f g h

Jogador atual: Brancas
Digite a origem: e2
Digite o destino: e4
...
```

---

## Como Usar

1. **Clone o repositório:**
   ```bash
   git clone https://github.com/MateusPaulino13/Xadrez-Console.git
   cd Xadrez-Console
   ```

2. **Compile o projeto:**
   - Se estiver usando o .NET CLI:
     ```bash
     dotnet build
     ```

3. **Execute o projeto:**
   ```bash
   dotnet run
   ```

4. **Siga as instruções exibidas no console para jogar!**

---

## Requisitos

- [.NET SDK](https://dotnet.microsoft.com/download) (recomendado .NET 6 ou superior)
- Terminal ou console de sua preferência

---

## Estrutura do Projeto

```
Xadrez-Console/
├── src/
│   ├── tabuleiro/
│   │   ├── Tabuleiro.cs
│   │   ├── Posicao.cs
│   │   └── Cor.cs
│   ├── xadrez/
│   │   ├── PartidaDeXadrez.cs
│   │   ├── Peca.cs
│   │   ├── Peao.cs
│   │   ├── Torre.cs
│   │   ├── Cavalo.cs
│   │   ├── Bispo.cs
│   │   ├── Rainha.cs
│   │   └── Rei.cs
│   └── Program.cs
└── README.md
```

- **tabuleiro/**: Componentes genéricos do tabuleiro e posições.
- **xadrez/**: Peças, regras, lógica de xadrez.
- **Program.cs**: Entrada principal do programa.

---

## Possíveis Melhorias

- Implementar suporte a roque, en passant e empate por repetição/50 lances.
- Adicionar histórico de movimentos.
- Suporte a partidas contra IA.
- Sistema de configuração de partidas (cores, tempo, etc.)
- Interface gráfica (GUI) futura.

---

## Contribuições

Contribuições são muito bem-vindas! Sinta-se à vontade para:

- Abrir issues para relatar bugs ou sugerir melhorias
- Criar pull requests com correções ou novas funcionalidades
- Discutir ideias para evolução do projeto

Veja o arquivo [CONTRIBUTING.md](CONTRIBUTING.md) (caso exista) para mais detalhes.

---

## Licença

Este projeto está sob a licença MIT. Veja o arquivo [LICENSE](LICENSE) para mais informações.

---

## Autor

Feito com 💙 por [Mateus Paulino](https://github.com/MateusPaulino13)

---
