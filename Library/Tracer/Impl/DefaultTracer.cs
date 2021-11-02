using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Library.Model;
using Thread = System.Threading.Thread;

namespace Library.Tracer.Impl
{
    public class DefaultTracer : ITracer
    {
        private readonly TraceResult _traceResult = new();

        public void StartTrace()
        {
            var threadId = Thread.CurrentThread.ManagedThreadId;
            var thread = _traceResult.Threads.GetOrAdd(threadId, new Model.Thread(threadId));
            var trace = new StackTrace();
            var methodFullName = trace.ToString().Split('\n')[1];
            var method = new Method(GetClassName(methodFullName), GetMethodName(methodFullName));
            method.Time = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
            AddUncompletedMethod(method, thread.Methods);
        }

        public void StopTrace()
        {
            var threadId = Thread.CurrentThread.ManagedThreadId;
            var thread = _traceResult.Threads.GetOrAdd(threadId, new Model.Thread(threadId));
            var method = GetLastUncompletedMethod(thread.Methods);
            method.Time = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond - method.Time;
            method.IsCompleted = true;
        }

        public TraceResult GetTraceResult()
        {
            SetThreadsTimes();
            return _traceResult;
        }

        private void SetThreadsTimes()
        {
            foreach (var thread in _traceResult.Threads.Values)
            {
                long time = 0;
                foreach (var method in thread.Methods) time += method.Time;

                thread.Time = time;
            }
        }

        private string GetMethodName(string methodFullName)
        {
            var firstLetterIndex = methodFullName.LastIndexOf('.') + 1;
            return methodFullName.Substring(firstLetterIndex,
                methodFullName.IndexOf('(') - firstLetterIndex);
        }

        private string GetClassName(string methodFullName)
        {
            var firstLetterIndex = methodFullName.LastIndexOf('.') + 1;
            methodFullName = methodFullName.Substring(0, firstLetterIndex - 1);
            return methodFullName.Substring(methodFullName.LastIndexOf('.') + 1);
        }

        private void AddUncompletedMethod(Method method, List<Method> methods)
        {
            if (methods.Count != 0 && !methods.Last().IsCompleted)
                AddUncompletedMethod(method, methods.Last().Methods);
            else
                methods.Add(method);
        }

        private Method GetLastUncompletedMethod(List<Method> methods)
        {
            if (IsAllInnerMethodsCompleted(methods.Last())) return methods.Last();

            return GetLastUncompletedMethod(methods.Last().Methods);
        }

        private bool IsAllInnerMethodsCompleted(Method method)
        {
            foreach (var m in method.Methods)
                if (!m.IsCompleted)
                    return false;

            return true;
        }
    }
}