using System;
using System.Threading;
using System.Threading.Tasks;

namespace _002_TPL_ReturnValue
{
    internal class Program
    {
        private static bool flag = false;


        static void Main(string[] args)
        {
            Task<int> task = new Task<int>(GetIntResult);
            task.Start();
            Console.WriteLine($"first assync aperation have result is {task.Result}");

            Thread.Sleep(1000);

            TaskFactory taskFactory = new TaskFactory();
            Task<bool> task2 = taskFactory.StartNew(GetBoolResult);
            Console.WriteLine($"Second assync aperation have result is {task2.Result}");

            Thread.Sleep(1000);

            Task<bool> task3 = Task.Run(GetBoolResult);
            Console.WriteLine($"Third assync aperation have result is {task3.Result}");
        }

        public static int GetIntResult()
        {
            return 1;
        }

        public static bool GetBoolResult()
        {
            if (flag)
            {
                flag = false;
                return true;
            }
            else
            {
                flag = true;
                return false;
            }
        }
    }
}
