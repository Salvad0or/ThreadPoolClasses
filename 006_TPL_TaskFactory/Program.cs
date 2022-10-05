using System;
using System.Threading;
using System.Threading.Tasks;

namespace _006_TPL_TaskFactory
{
    internal class Program
    {
        delegate int Exc(int a, string s);

        static void Main(string[] args)
        {

            TaskFactory taskFactory = new TaskFactory();

            double b = 0;

            Task<double> task1 = taskFactory.StartNew(() => { return Method1(b); });
            b++;
            Task<double> task2 = taskFactory.StartNew(() => { return Method1(b); });      
            b++;
            Task<double> task3 = taskFactory.StartNew(() => { return Method1(b); });     
            b++;
            Task<double> task4 = taskFactory.StartNew(() => { return Method1(b); });
            b++;

            Task[] massTask = { task1, task2, task3, task4 };

            taskFactory.ContinueWhenAll(massTask, completedTask =>

            {
                double a = default;

                foreach (Task<double> item in completedTask)
                {
                    a += item.Result;
                }

                Console.WriteLine($"Result is {a}");

                
            });

            Console.ReadKey();

        }


        static double Method1(object a)
        {

            Console.WriteLine($"Id задачи :{Task.CurrentId} , Номер потока : {Thread.CurrentThread.ManagedThreadId}");

            double result = default;

            try
            {
                result = (double)a;
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
                return 0;
            }

            return result * 2 / new Random().NextDouble();
        }
    }
}
