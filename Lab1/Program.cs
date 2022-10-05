using System.Text;
using Lab1.Tracer;
using Lab1.Serialization;

internal class Program
{
    private static ITracer _tracer = new Tracer();

    public static void Main()
    {
        Foo foo = new Foo(_tracer);

        var task1 = new Task(() => {
            foo.MyMethod();
            foo.MyMethod();
            foo.NoInner();
            foo.DoubleInner();
        });

        var task2 = new Task(() => foo.MyMethod());
        var task3 = new Task(() => foo.MyMethod());

        task1.Start();
        task2.Start();
        task3.Start();

        task1.Wait();
        task2.Wait();
        task3.Wait();

        var result = _tracer.GetTraceResult();

        var jsonResult = (new JsonTraceSerializer()).Serialaze(result);
        var xmlResult = (new XmlTraceSerializer()).Serialaze(result);
        Console.WriteLine(jsonResult);
        Console.WriteLine(xmlResult);

        using (var writer = new StreamWriter("result.json", false, Encoding.UTF8))
        {
            writer.Write(jsonResult);
        }
        using (var writer = new StreamWriter("result.xml", false, Encoding.UTF8))
        {
            writer.Write(xmlResult);
        }
    }
}

class Foo
{
    private Bar _bar;
    private ITracer _tracer;

    public Foo(ITracer tracer)
    {
        _tracer = tracer;
        _bar = new Bar(tracer);
    }

    public void MyMethod()
    {
        _tracer.StartTrace();

        _bar.InnerMethod();

        _tracer.StopTrace();
    }

    public void NoInner()
    {
        _tracer.StartTrace();

        Thread.Sleep(500);

        _tracer.StopTrace();
    }

    public void DoubleInner()
    {
        _tracer.StartTrace();

        _bar.InnerMethod();

        _bar.InnerMethod();

        _tracer.StopTrace();
    }
}

class Bar
{
    private ITracer _tracer;

    public Bar(ITracer tracer)
    {
        _tracer = tracer;
    }

    public void InnerMethod()
    {
        _tracer.StartTrace();

        Thread.Sleep(100);

        _tracer.StopTrace();
    }
}