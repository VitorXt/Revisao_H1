using System;
class Program
{
    static int Soma(int n)
    {
        if (n == 1)
            return 1;
        else
            return (2 * n - 1) + Soma(n - 1);
    }

    static void Main(string[] args)
    {
        Console.Write("Digite um número para fazer a soma: ");
        int numero = Convert.ToInt32(Console.ReadLine());

        int resultado = Soma(numero);
        Console.WriteLine("O resultado da soma é: " + resultado);
    }
}