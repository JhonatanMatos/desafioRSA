﻿// See https://aka.ms/new-console-template for more information

using System.Diagnostics;
using System.Numerics;

try
{
    //teste
    #region Cronometro

    Stopwatch stopWatch = new Stopwatch();
    stopWatch.Start();

    #endregion

    #region Define Mensagem a ser Criptografada

    string msg = "The information security is of significant importance to ensure the privacy of communications";
    //Console.WriteLine("Digite a mensagem que deseja criptogravar:");
    //msg = Console.ReadLine();
    Console.WriteLine();
    Console.WriteLine("Mensagem = " + msg);
    Console.WriteLine();
    string msgCrip = "";
    string msgDecrip = "";

    #endregion

    #region Etapa 1 - Escolher p e q (números primos) para o cálculo de N = p.q
    Random r = new Random();
    Random s = new Random();
    int p = r.Next(1, 100);
    int q = s.Next(1, 100);

    //aqui testa se não é primo, e executa enquanto não for primo
    while (!ePrimo(p))
    {
        p = r.Next(1, 100);
    }
    while (!ePrimo(q))
    {
        q = s.Next(1, 100);
    }
    //p = 17; q = 41;//00:00:00.0496571
    //p = 6569; q = 13033;

    #endregion

    #region Etapa 2 - Calcular a função totiente tot(N) = (p-1).(q-1) 

    int n = p * q;
    int totN = (p - 1) * (q - 1);

    #endregion

    #region Etapa 3 - Escolha 1 < e < tot(N), tal que e e tot(N)sejam primos entre si

    int e = 1;
    bool verdadeiro = false;
    while (!verdadeiro)
    {
        int a = MDC(totN, e);
        if (a == 1)
        {
            verdadeiro = true;
        }
        else
        {
            e++;
        }
    }

    #endregion

    #region Etapa 4 - Escolha d tal que e.d mod (N) =1

    verdadeiro = false;
    int d = 1;
    while (!verdadeiro)
    {
        int a = (e * d) % totN;
        if (a == 1)
        {
            verdadeiro = true;
        }
        else
        {
            d++;
        }
    }
    #endregion


    #region Exibe Valor das Variaveis 

    Console.WriteLine("p: {0}", p);
    Console.WriteLine("q: {0}", q);
    Console.WriteLine("n: {0}", n);
    Console.WriteLine("e: {0}", e);
    Console.WriteLine("d: {0}", d);
    Console.WriteLine();

    #endregion

    #region Chaves Assimetricas

    #region Criptografar - Chave Pública (e,N) => C = P^e mod N    
    int valorLetra = 0;
    BigInteger novaLetra = 0;
    foreach (char letra in msg)
    {
        valorLetra = ((byte)letra);
        novaLetra = BigInteger.Pow((valorLetra), (e)) % n;
        msgCrip += novaLetra + " ";
    }
    Console.WriteLine("Mensagem Criptografada = " + msgCrip.Remove(msgCrip.Length - 1));
    Console.WriteLine();
    #endregion

    #region Decriptografar - Chave Privada (d,N) => P = C^d mod N
    BigInteger voltaLetra;
    foreach (string valor in msgCrip.Remove(msgCrip.Length - 1).Split(" "))
    {
        voltaLetra = BigInteger.Pow((Convert.ToInt32(valor)), (d)) % n;
        msgDecrip += ((char)voltaLetra);
    }
    Console.WriteLine("Mensagem Decriptografada = " + msgDecrip);
    Console.WriteLine();
    stopWatch.Stop();
    Console.WriteLine("Tempo Decorrido: {0}", stopWatch.Elapsed);
    Console.ReadLine();


    #endregion

    #endregion
}
catch (Exception e)
{
    Console.WriteLine("{ 0} Exception caught.", e);
}

#region Metodos

#region Máximo divisor comum

int MDC(int a, int b)
{
    int resto;
    while (b != 0)
    {
        resto = a % b;
        a = b;
        b = resto;
    }
    return a;
}
#endregion

#region Valida Numero Primo
bool ePrimo(int num)
{
    int div = 0;

    for (int i = 1; i <= num; i++)
    {
        if (num % i == 0)
        {
            div++;
        }
    }

    return div == 2; //true se div for 2, falso caso contrário
}
#endregion

#endregion