# Xadrez de Console
![image](https://github.com/Foqsz/xadrez-de-console/assets/96602671/20a935c2-7412-40e7-be7c-9c18a6386ada)

## Índice
1. [Sobre o projeto](#sobre-o-projeto)
2. [Sobre o tabuleiro](#sobre-o-tabuleiro)
3. [Impressão do tabuleiro no console](#impressão-do-tabuleiro-no-console)
4. [A exceção tabuleiroException](#a-exceção-tabuleiroexception)
5. [Sobre as peças](#sobre-as-peças)
6. [Peças presentes no tabuleiro](#peças-presentes-no-tabuleiro)
7. [Método para colocar as peças no tabuleiro](#método-para-colocar-as-peças-no-tabuleiro)
8. [Como foi criada a restrição de movimento para cada peça](#como-foi-criada-a-restrição-de-movimento-para-cada-peça)
9. [Validar posição de origem](#validar-posição-de-origem)
10. [Validar posição de destino](#validar-posição-de-destino)
11. [Jogadas especiais](#jogadas-especiais) 
12. [Contato](#contato)

## Sobre o projeto
Este projeto foi feito durante o curso de C# ministrado por Nélio Alves. O objetivo central é empregar os conhecimentos adquiridos na linguagem C# para desenvolver um jogo de Xadrez funcional que possa ser executado via console. Embora possa parecer uma tarefa simples à primeira vista, a complexidade subjacente a este projeto é significativa.

## Sobre o tabuleiro
### Impressão do tabuleiro no console
A impressão do tabuleiro foi uma parte simples, porém essencial, do projeto. Utilizou-se um loop for para criar uma representação visual do tabuleiro no console, composta por um padrão de "-" com espaços entre eles, formando um tabuleiro de oito por oito casas, totalizando sessenta e quatro espaços, conforme o tamanho padrão de um tabuleiro de Xadrez.

```csharp
        public static void imprimirTabuleiro(Tabuleiro tab, bool[,] posicoePossiveis)
        {

            ConsoleColor fundoOriginal = Console.BackgroundColor;
            ConsoleColor fundoAlterado = ConsoleColor.DarkGray;

            for (int i = 0; i < tab.linhas; i++)
            {
                Console.Write(8 - i + " ");
                for (int j = 0; j < tab.colunas; j++)
                {
                    if (posicoePossiveis[i, j])
                    {
                        Console.BackgroundColor = fundoAlterado;
                    }
                    else
                    {
                        Console.BackgroundColor = fundoOriginal;
                    }
                    imprimirPeca(tab.peca(i, j));
                    Console.BackgroundColor = fundoOriginal;
                }
                Console.WriteLine();
            }
            Console.WriteLine("  a b c d e f g h");
            Console.BackgroundColor = fundoOriginal;
        }
```
Com o resultado final: 

![image](https://github.com/Foqsz/xadrez-de-console/assets/96602671/03b08c17-fce7-434d-8007-a4d62facde16)

<!-- Pular linha -->
---

### A exceção `TabuleiroException`

Durante o desenvolvimento do jogo, foi essencial criar uma exceção específica para lidar com situações em que uma casa inválida é selecionada pelo jogador. Essa exceção é chamada `TabuleiroException`.

#### Exemplos de uso:

- Quando não existe uma peça na posição de origem selecionada:
```csharp
throw new TabuleiroException("Não existe peça na posição de origem escolhida!");
```

- Quando a peça selecionada não pertence ao jogador atual:
```csharp
throw new TabuleiroException("A peça de origem escolhida não é sua!");      
```

- Quando não há movimentos possíveis para a peça selecionada:
```csharp
throw new TabuleiroException("Atencao: Não há movimentos possíveis para a peça de origem escolhida!");     
```

- Quando a posição de destino escolhida é inválida:
```csharp
throw new TabuleiroException("Posição de destino inválida!");
```

---

Essa exceção é fundamental para manter a integridade e a lógica do jogo, garantindo que o jogador receba feedback adequado caso tente realizar movimentos inválidos.

### Sobre as peças

No tabuleiro de Xadrez, existem diferentes tipos de peças, cada uma com suas próprias características e movimentos específicos. Abaixo estão as peças presentes no jogo, indicando a quantidade de peças de cada tipo e como são representadas:

- **Rei:** Uma única peça de cada cor (branca e preta), representada pela letra R.
- **Dama:** Uma única peça de cada cor (branca e preta), representada pela letra D.
- **Bispo:** Duas peças de cada cor (branca e preta), representadas pela letra B.
- **Cavalo:** Uma única peça de cada cor (branca e preta), representada pela letra C.
- **Torre:** Uma única peça de cada cor (branca e preta), representada pela letra T.
- **Peão:** Oito peças de cada cor (branca e preta), representadas pela letra P.

Essas peças compõem o conjunto básico do Xadrez e cada uma desempenha um papel fundamental na estratégia e na dinâmica do jogo.

Imagem do tabuleiro com as peças:

![image](https://github.com/Foqsz/xadrez-de-console/assets/96602671/83be9562-f898-4e11-a456-2d9829a9ae6d)
<!-- Pular linha -->

### Método para colocar as peças no tabuleiro

Durante o desenvolvimento do jogo de Xadrez em C#, foi implementado um método para colocar novas peças em posições específicas do tabuleiro. Esse método é essencial para configurar o tabuleiro com as peças necessárias para iniciar uma partida.

Aqui está o método `colocarNovaPeca`:

```csharp
public void colocarNovaPeca(char coluna, int linha, Peca peca)
{
    tab.colocarPeca(peca, new PosicaoXadrez(coluna, linha).toPosicao());
    pecas.Add(peca);
}
```

Este método recebe três parâmetros:
- `coluna`: A coluna onde a peça será colocada, representada por um caractere.
- `linha`: A linha onde a peça será colocada, representada por um número.
- `peca`: A peça que será colocada no tabuleiro.

Internamente, o método converte a posição da peça (`coluna` e `linha`) em uma posição de matriz utilizando a classe `PosicaoXadrez` e, em seguida, utiliza o método `colocarPeca` do tabuleiro para colocar a peça na posição desejada. Além disso, a peça é adicionada à lista de peças (`pecas`) para acompanhamento e controle durante a partida.

Esse método é crucial para configurar o tabuleiro com as peças necessárias para iniciar o jogo de Xadrez, permitindo que as partidas sejam inicializadas com qualquer disposição desejada das peças.

#### Funcionamento:
1. Converte a posição informada (coluna e linha) em uma posição válida do tabuleiro, utilizando a classe `PosicaoXadrez` e seu método `toPosicao()`.
2. Coloca a peça na posição calculada no passo anterior, utilizando o método `colocarPeca` do objeto `tabuleiro`.
3. Adiciona a peça à lista de peças (`pecas`).

Este método é essencial para configurar o tabuleiro com as peças corretas antes do início de uma partida de Xadrez.

### Como foi criada a restrição de movimento para cada peça

Durante o desenvolvimento do jogo de Xadrez em C#, foram criados métodos específicos na classe `PartidaXadrez` para validar as posições de origem e destino das peças, aplicando as restrições de movimento adequadas para cada tipo de peça.

#### Método `validarPosicaoDeOrigem`

Este método é responsável por verificar se a posição de origem selecionada pelo jogador é válida para movimentar uma peça. Ele realiza as seguintes verificações:

1. **Posição nula:** Verifica se não há uma peça na posição de origem escolhida.

```csharp
if (tab.peca(pos) == null)
{
    throw new TabuleiroException("Não existe peça na posição de origem escolhida!");
}
```

2. **Cor da peça:** Verifica se a peça selecionada pertence ao jogador atual, garantindo que o jogador só possa mover suas próprias peças.

```csharp
if (jogadorAtual != tab.peca(pos).cor)
{
    throw new TabuleiroException("A peça na posição de origem escolhida não é sua!");
}
```

3. **Movimentos possíveis:** Verifica se a peça selecionada possui movimentos possíveis. Se não houver movimentos disponíveis para a peça selecionada, uma exceção é lançada.

```csharp
if (!tab.peca(pos).existeMovimentosPossiveis())
{
    throw new TabuleiroException("Atencao: Não há movimentos possíveis para a peça de origem escolhida!");
}
```

#### Método `validarPosicaoDeDestino`

Este método é responsável por validar se a posição de destino escolhida pelo jogador é válida para mover a peça selecionada. Ele realiza a seguinte verificação:

1. **Movimento válido:** Verifica se a peça selecionada pode se mover para a posição de destino especificada. Se a peça não puder se mover para a posição de destino, uma exceção é lançada.

```csharp
if (!tab.peca(origem).movimentoPossivel(destino))
{
  throw new TabuleiroException("Posição de destino inválida!");
}
```

Esses métodos garantem que as peças só possam ser movidas de acordo com as regras do jogo de Xadrez, mantendo a integridade e a lógica do jogo durante as partidas.

### Jogadas especiais

Neste tópico, serão detalhadas as jogadas especiais implementadas no jogo de Xadrez em C#.

1. **Roque Pequeno:** O roque pequeno ocorre quando o rei e uma torre ainda não se moveram na partida, e entre eles existem duas casas vazias. O código para esta jogada especial é o seguinte:

    ```csharp
            // #jogadaespecial roque pequeno
            if (p is Rei && destino.coluna == origem.coluna + 2)
            {
                Posicao origemT = new Posicao(origem.linha, origem.coluna + 3);
                Posicao destinoT = new Posicao(origem.linha, origem.coluna + 1);
                Peca T = tab.retirarPeca(origemT);
                T.incrementarQteMovimentos();
                tab.colocarPeca(T, destinoT);
            }
    ```

2. **Roque Grande:** O roque grande ocorre quando o rei e uma torre ainda não se moveram na partida, e entre eles existem quatro casas vazias. O código para esta jogada especial é o seguinte:

    ```csharp
            // #jogadaespecial roque grande
            if (p is Rei && destino.coluna == origem.coluna - 2)
            {
                Posicao origemT = new Posicao(origem.linha, origem.coluna - 4);
                Posicao destinoT = new Posicao(origem.linha, origem.coluna - 1);
                Peca T = tab.retirarPeca(origemT);
                T.incrementarQteMovimentos();
                tab.colocarPeca(T, destinoT);
            }
    ```

3. **En Passant:** O En Passant ocorre quando um peão adversário avança duas casas em seu primeiro movimento para evitar um confronto com um peão avançado. Um peão pode realizar a captura do peão adversário da mesma forma. O código para esta jogada especial é o seguinte:

    ```csharp
            //#jogadaespecial en passant

            if (p is Peao)
            {
                if (origem.coluna != destino.coluna && pecaCapturada == null)
                {
                    Posicao posP;
                    if (p.cor == Cor.Branca)
                    {
                        posP = new Posicao(destino.linha + 1, destino.coluna);
                    }
                    else
                    {
                        posP = new Posicao(destino.linha - 1, destino.coluna);
                    }
                    pecaCapturada = tab.retirarPeca(posP);
                    capturadas.Add(pecaCapturada);
                }
            } 
    ```

4. **Promoção:** A jogada de promoção ocorre quando um peão alcança a última fileira do tabuleiro adversário. Neste caso, virará uma Dama.

    ```csharp
            // #jogadaespecial promocao
            if (p is Peao)
            {
                if ((p.cor == Cor.Branca && destino.linha == 0) || (p.cor == Cor.Preta && destino.linha == 7))
                {
                    p = tab.retirarPeca(destino);
                    pecas.Remove(p);
                    Peca dama = new Dama(tab, p.cor);
                    tab.colocarPeca(dama, destino);
                    pecas.Add(dama);
                }
            }
    ```

Contato:
- Email: contatovictorvinicius05@gmail.com
- LinkedIn: [Victor Vinicius](https://www.linkedin.com/in/victor-vinicius-2a9166255/)
