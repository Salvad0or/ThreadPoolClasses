using System;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp5
{
    internal class Program
    {
        static void Main(string[] args)
        {
            #region Передавыемые значения через структуры
            int a = 7 , b = 9;
            
            Box box;
            box.a = a;
            box.b = b;

            Task<int> task = new Task<int>(Calc, box);
            task.Start();

            for (int i = 0; i < 5; i++)
            {
                Thread.Sleep(1000);
                Console.WriteLine("Что-то происходит в Мейне");
            }

            Console.WriteLine($"Результат ассинхронной операции : {task.Result} ");

            #endregion

            #region Передаваемые значения через =>

            int c = 7;
            int d = 8;

            Task<int> task2 = new Task<int>(() => c + d);
            Task<int> task3 = new Task<int>(() => Calc(7, 12)); // Можно и так
            

            task2.Start();

            Console.WriteLine($"Результат второй операции через лямбду : {task2.Result}");

            #endregion
        }

        static int Calc(object arg)
        {
            Box box = (Box)arg;

            return box.a + box.b;
        }

        static int Calc(int a, int b)
        {
            return a - b;
        }

        struct Box
        {
            public int a;
            public int b;
        }
    }
}
