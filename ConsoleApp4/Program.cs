
Test t = new Test();

ThreadPool.QueueUserWorkItem(new WaitCallback(t.Method1));


void Method2()
{
    
}


class Test
{
    
    public void Method1(object a)
    {
        for (int i = 0; i < 10; i++)
        {
            Console.Write("Method1");
            Thread.Sleep(1000);
        }
    }
}


class ThreadPoolWorker

{

    public bool Flag { get; set; } = false;

    private readonly Action<object> deleg;

    public ThreadPoolWorker(Action<object> action)
    {
        deleg = a;
    }

    void Start()
    {
        ThreadPool.QueueUserWorkItem(new WaitCallback(deleg));
        Flag = true;
    }

    public void Wait()
    {

    }


}

