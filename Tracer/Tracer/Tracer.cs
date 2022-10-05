using Lab1.Tracer.TraceResults;
using Lab1.Tracer.TraceResults.Buffers;
using Lab1.Tracer.TraceResults.ReadOnly;
using System.Collections.Concurrent;
using System.Diagnostics;

namespace Lab1.Tracer
{
    public class Tracer : ITracer
    {

        private ConcurrentDictionary<int, ThreadTraceResult> _threads;

        public Tracer()
        {
            _threads = new ConcurrentDictionary<int, ThreadTraceResult>();
        }

        public TraceResult GetTraceResult()
        {
            List<ReadOnlyThreadTraceResult> threads = new();

            foreach (int threadId in _threads.Keys)
            {
                threads.Add(new ReadOnlyThreadTraceResult(threadId, _threads[threadId].Methods));
            }

            return new TraceResult(threads);
        }

        public void StartTrace()
        {
            int threadId = Environment.CurrentManagedThreadId;
            _threads.GetOrAdd(threadId, new ThreadTraceResult());

            var stackTrace = new StackTrace();
            var method = stackTrace.GetFrame(1)?.GetMethod();

            MethodTraceResult methodInfo = new()
            {
                MethodName = method.Name,
                ClassName = method.DeclaringType.Name
            };
            methodInfo.Stopwatch.Start();

            _threads[threadId].RunningMethods.Push(methodInfo);
        }

        public void StopTrace()
        {
            int threadId = Environment.CurrentManagedThreadId;

            MethodTraceResult method = _threads[threadId].RunningMethods.Pop();

            method.Stopwatch.Stop();

            ReadOnlyMethodTraceResult methodInfo = new(
                method.ClassName,
                method.MethodName,
                method.Stopwatch.ElapsedMilliseconds,
                method.ChildMethods
            );

            if (_threads[threadId].RunningMethods.Count == 0)
            {
                _threads[threadId].Methods.Add(methodInfo);
            }
            else
            {
                _threads[threadId].RunningMethods.Peek().ChildMethods.Add(methodInfo);
            }
        }
    }
}
