using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
//using System.Threading.Tasks;


namespace Summa_Async
{
    class Program
    {
        //static void Main(string[] args)
        static void Summa(int n, CancellationToken token)
        {
            int result = 1;
            for (int i = 1; i <= n; i++)
            {
                if (token.IsCancellationRequested)
                {
                    Console.WriteLine("Операция прервана токеном");
                    return;
                }
                result += i;
                Console.WriteLine($"Сумма {i} чисел  равна {result}");
                Thread.Sleep(1000);
            }
        }
        // определение асинхронного метода
        static async void SummaAsync(int n, CancellationToken token)
        {
            if (token.IsCancellationRequested)
                return;
            await Task.Run(() => Summa(n, token));
        }

        static void Main(string[] args)
        {
            CancellationTokenSource cts = new CancellationTokenSource();
            CancellationToken token = cts.Token;
            Console.WriteLine("Введите количество чисел: ");
            Console.ReadLine(int m);
            SummaAsync(m, token);
            Thread.Sleep(3000);
            cts.Cancel();
            Console.Read();
        }
    }
}
