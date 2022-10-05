using System;
using System.Threading;
using System.Threading.Tasks;

namespace _007_ValueTask
{
    internal class Program
    {
        static void Main(string[] args)
        {

            int a = Sum(0, 2).Result;

            Console.Write(a);

            Console.ReadKey();


        }


        static ValueTask<int> Sum(int a, int b)
        {
            Console.WriteLine($"Номер задачи {Task.CurrentId}, номер потока : {Thread.CurrentThread.ManagedThreadId}");

            if (a == 0) return new ValueTask<int>(a);
            else if ( b== 0)return new ValueTask<int>(b);

            return new ValueTask<int>(a + b);
            
        }
    }
}
