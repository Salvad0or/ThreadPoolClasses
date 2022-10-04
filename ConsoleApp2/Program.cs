using System;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Action action = new Action(ThreadOutPut);

            Task task = new Task(action);

            task.Start();
            MainOutPut();

            /// ^
            /// |
            /// так называемая холодная задача
            /// 

            Task.Run(ThreadOutPut);
            MainOutPut();

            /// ^
            /// |
            /// Самая популярная горячая задача
            /// 
        }

        static void MainOutPut()
        {
            for (int i = 0; i < 40; i++)
            {
                Console.Write("M");
                Thread.Sleep(75);
            }
        }

        static void ThreadOutPut()
        {
            for (int i = 0; i < 40; i++)
            {
                Console.Write("T");
                Thread.Sleep(50);
            }
        }
    }
}
