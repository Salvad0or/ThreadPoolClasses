using System;
using System.Threading;

namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Main started");

            ThreadPoolWorker<int> worker = new ThreadPoolWorker<int>(Method1);

            worker.Start(10);

            Thread.Sleep(100);

            while (worker.Continued == false)
            {
                Console.WriteLine("Method will be continued");
                Thread.Sleep(200);
            }

            Console.WriteLine($"Method finished his work, result is {worker.Result}");

            Console.ReadKey();
        }


        static int Method1(object s)
        {
            int result = (int) s;

            for (int i = 0; i < 10; i++)
            {
                result++;
                Thread.Sleep(100);
            }

            return result;
        }
    }


    class ThreadPoolWorker<Tresult>

    {
        private readonly Func<object, Tresult> Tpw;

        private Tresult result;
        public Tresult Result
        {
            get
            {
                while (Continued != true) Thread.Sleep(150);
                return Succesed == true && ex == null ? result : throw ex;
                
            }
        }

        public bool Succesed { get; private set; } = false;
        public bool Continued { get; private set; } = false;

        public Exception ex { get; private set; } = null;

        public ThreadPoolWorker(Func<object, Tresult> s)
        {
            Tpw = s ?? throw new Exception(nameof(s));
        }

        public void Start(object res)
        {
            ThreadPool.QueueUserWorkItem(new WaitCallback(Work), res);
            result = default;
        }

        private void Work(object s)
        {
            try
            {
                result = Tpw.Invoke(s);
                Succesed = true;
            }
            catch (Exception ex)
            {
                this.ex = ex;
                Succesed = false;
                Console.WriteLine(this.ex.Message);
            }
            finally
            {
                Continued = true;
            }
        }

        void Wait()
        {
            while (Continued != true) Thread.Sleep(150);
            if (Succesed = false) throw new Exception();       
        }



    }
}
