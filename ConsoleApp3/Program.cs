using System;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp3
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Method started his work");

            Task task = new Task(new Action(AssyncMethod));

            task.Start();

            Thread.Sleep(1000);

            task.Wait(); // если этот метод убрать, то метод мейн завершит свою работу не дожидаясь выполнения задачи.

            // Но крайне не рекомендуется использование данных методов т.к. они вызывают блокировки.
            // WaitAll and WaitAny - это по сути то же самое, разница только в том, что первый ждет выполнение всех задач
            // А второй ждет выполнения любой задачи на выполнение
           
            Console.WriteLine("Methos finshed his work");
        }

        static void AssyncMethod()
        {
            for (int i = 0; i < 100; i++)
            {
                Console.WriteLine("Assync Method sleep and he wait for MainMethod");
                Thread.Sleep(1000);

            }
        }
    }
}
