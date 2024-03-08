using System;
using tabuleiro;
using xadrez;
internal class Program
{
    static void Main(string[] args)
    {
        try
        {
            Tabuleiro tab = new Tabuleiro(8, 8);

            tab.colocarPeca(new Torre(tab, Cor.Preta), new Posicao(0, 0));
            tab.colocarPeca(new Torre(tab, Cor.Preta), new Posicao(1, 9));
            tab.colocarPeca(new Rei(tab, Cor.Preta), new Posicao(0, 2));

            Tela.imprimirTabuleiro(tab);
        }
        catch (TabuleiroException e)
        {
            Console.WriteLine(e.Message);
        }
        Console.ReadLine();
    }
}