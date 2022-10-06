using System;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp7
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Task<int> calculate = MethodAsync(10);

            while (!calculate.IsCompleted)
            {
                Console.WriteLine("*");
                Thread.Sleep(50);
            }

            calculate.ContinueWith((t) => Console.WriteLine(t.Result));

            Console.ReadKey();
        }

        static Task<int> MethodAsync(int a)
        {
            return Task.Run(() => MethodSum(a));
        }

        static int MethodSum(int a)
        {
            for (int i = 1; i < 10; i++)
            {
                Thread.Sleep(200);
                a *= i;
            }

            return a;
        }
    }
}
