//using Castle.DynamicProxy;
using GenericService.ServiceLogger;
using System;
using System.Diagnostics;

namespace GenericRepository.EntityFramework.SampleWebApi.Intercetors
{
    /*
    public class MethodInterceptors : IInterceptor
    {
        private readonly ILogger _logger;
        public MethodInterceptors(ILogger logger)
        {
            _logger = logger;
        }

        public void Intercept(IInvocation invocation)
        {
            _logger.Log("Performance", $"{invocation.Method.Name} is called at {DateTime.Now}", GenericService.ServiceLogger.TraceLevel.Info);
            var sw = Stopwatch.StartNew();
            invocation.Proceed();
            sw.Stop();
            _logger.Log("Performance", $"{invocation.Method.Name} took {sw.ElapsedMilliseconds} milliseconds to execute",GenericService.ServiceLogger.TraceLevel.Info);
        }
    }*/
}