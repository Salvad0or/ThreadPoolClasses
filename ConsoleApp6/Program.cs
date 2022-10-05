using System;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp6
{
    internal class Program
    {
        static void Main(string[] args)
        {
            #region Это база
            Task task = new Task(() =>
            {
                Console.WriteLine("Метод выполняется, погоди 3 сек");
                Thread.Sleep(3000);

            });

            Task continuetion = task.ContinueWith(o => Console.WriteLine("А вот и продолжение")); // Метод запустится автоматически после завершения
                                                                                                  // работы task
            task.Start();

            Console.ReadKey();

            #endregion

            #region Пример посложнее

            int a = 2, b = 3;

            Task<int> task3 = Task.Run(() => Calc(a, b));

            task3.ContinueWith(Continuation);
            Console.ReadKey();

            /// Здесь мы используем метод продолжения передавая результаты задачи дальше по цепочке
            /// Метод Continuation принимает параметр Task<int> t, без принимаемого параметра не получится продолжить

            #endregion

            ///как делать красивые и лаконичные продолжения :

            Task task5 = Task.Run(() => Console.WriteLine("Method1"));

            task5.ContinueWith((Task k) => Console.WriteLine("Method2"))
                 .ContinueWith((Task k) => Console.WriteLine("Method3"));

            // Здесь можно использовать и возвращяемые значения, опишу ниже :

            int d = 0;
            int e = 0;

            int res;

            Task<int> task6 = Task.Run(() => d + e);

            task6.ContinueWith((Task<int> k) => task6.Result + 1)
                 .ContinueWith((Task<int> k) => k.Result + 1)
                 .ContinueWith((Task<int> k) => res = k.Result)
                 .ContinueWith((Task<int> k) => Console.WriteLine($"Результат всех продолжений равен : {k.Result}"));
        }

        static int Calc(int a, int b)
        {
            Console.WriteLine($"Task Id : {Task.CurrentId}, ThreadId :{Thread.CurrentThread.ManagedThreadId}");
            return a + b;
        }

        static void Continuation(Task<int> t)
        {
            Console.WriteLine($"Task Id : {Task.CurrentId}, ThreadId :{Thread.CurrentThread.ManagedThreadId}");
            Console.WriteLine($"Результат операции: {t.Result} , {t.Status}");
        }
    }

  

}
