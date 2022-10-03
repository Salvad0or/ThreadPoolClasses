//Main1();
Main2();

#region Обертка ничего не возвращяет
void Main1()
{
    ThreadPoolWorker tpw = new ThreadPoolWorker(StarWriter);

    tpw.Start('*');


    for (int i = 0; i < 40; i++)
    {
        Console.Write("-");
        Thread.Sleep(50);
    }

    tpw.Wait();

    Console.WriteLine("Метод Main завершил свою работу");
}

void StarWriter(object a)
{
    char star = (char)a;

    for (int i = 0; i < 120; i++)
    {      
        Console.Write(star);
        Thread.Sleep(50);
    }
}

#endregion

#region Обертка возвращяет значение

void Main2()
{
    ThreadPoolWorkerGenerik<int> tpwg = new ThreadPoolWorkerGenerik<int>(SumNumber);

    tpwg.Start(1000);

    while (tpwg.Completed == false)
    {
        Console.Write("*");
        Thread.Sleep(100);
    }

    Console.Write("");

    Console.WriteLine($"Результат ассинхронной операции - {tpwg.Result}");
}


int SumNumber(object sender)
{
    int a = (int)sender;
    int sum = 0;

    for (int i = 0; i < 100; i++)
    {
        sum += a;
        Thread.Sleep(1);
    }
    return sum;
}

#endregion

/// <summary>
/// Обертка над пулом потоков
/// </summary>
class ThreadPoolWorker

{
    public bool Success { get; private set; } = false;
    public bool Completed { get; private set; } = false;
    public Exception Exception { get; private set; } = null;

    private readonly Action<object> action;

    public ThreadPoolWorker(Action<object> action)
    {
        this.action = action ?? throw new NullReferenceException(nameof(action));
    }

    public void Start(object state)
    {
        ThreadPool.QueueUserWorkItem(new WaitCallback(ThreadExecution), state);
        
    }


    public void ThreadExecution(object state)
    {
        try
        {
            action.Invoke(state);
            Success = true;
        }
        catch (Exception ex)
        {

            Exception = ex;
            Success = false;
        }
        finally
        {
            Completed = true;
        }
    }

    public void Wait()
    {
        while (Completed != true) Thread.Sleep(150);
        if (Exception != null) throw Exception;
        
    }


}
/// <summary>
/// Обертка над пулом потоков с обобщениями
/// </summary>
/// <typeparam name="TResult"></typeparam>
class ThreadPoolWorkerGenerik <TResult>
{
    private readonly Func<object, TResult> Func;

    private TResult result;

    public ThreadPoolWorkerGenerik(Func<object,TResult> func)
    {
        Func = func ?? throw new NullReferenceException(nameof(func));
        result = default;
    }

    public bool Success { get; private set; } = false;
    public bool Completed { get; private set; } = false;
    public Exception Exception { get; private set; } = null;

    /// <summary>
    /// когда кто то запросит результат
    /// ассинхронной операции
    /// в случае если операция не закончена мы будем спать
    /// </summary>
    public TResult Result
    {
        get
        {
            while (Completed == false) Thread.Sleep(150);

            return Success == true && Exception == null ? result : throw Exception;
        }
       
    }

    public void Start(object state)
    {
        ThreadPool.QueueUserWorkItem(new WaitCallback(ThreadExecution), state);
    }


    void ThreadExecution(object state)
    {
        try
        {
            result = Func.Invoke(state);
            Success = true;
        }
        catch (Exception ex)
        {
            Exception = ex;
            Success = false;

        }
        finally
        {
            Completed = true;
        }
    }

}
