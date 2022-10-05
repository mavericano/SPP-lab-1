using System;
using System.Threading;
using Lab1.Tracer;
using Lab1.Tracer.TraceResults;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestProject1;

[TestClass]
public class UnitTest1
{

    private ITracer tracer;
    private Foo foo;

    [TestInitialize]
    public void Init()
    {
        tracer = new Tracer();
        foo = new Foo(tracer);
    }
    
    [TestMethod]
    public void ShouldTimeBeInBounds()
    {
        foo.MyMethod();

        TraceResult traceResult = tracer.GetTraceResult();

        Assert.IsTrue(
            traceResult.Threads[0].Time > 100 && 
            traceResult.Threads[0].Time < 150
        );
        
       
    }

    [TestMethod]
    public void ShouldTimeOuterEqualTimeInner()
    {
        foo.MyMethod();
        
        TraceResult traceResult = tracer.GetTraceResult();
        
        Assert.AreEqual(
            traceResult.Threads[0].Methods[0].ExecutionTime,
            traceResult.Threads[0].Time
        );
    }

    [TestMethod]
    public void ShouldMethodNameBeCorrect()
    {
        foo.MyMethod();

        TraceResult traceResult = tracer.GetTraceResult();
        
        Assert.AreEqual("MyMethod", traceResult.Threads[0].Methods[0].MethodName);
    }

    [TestMethod]
    public void ShouldClassNameBeCorrect()
    {
        foo.MyMethod();

        TraceResult traceResult = tracer.GetTraceResult();

        Assert.AreEqual("Foo", traceResult.Threads[0].Methods[0].ClassName);
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