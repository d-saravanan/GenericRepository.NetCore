using Microsoft.AspNetCore.Mvc.Filters;
using System.Diagnostics;

namespace GenericRepository.EntityFramework.SampleWebApi.Insights
{
    public class ApiTimerAttribute : ActionFilterAttribute
    {
        private const string _timerKey = "__api_timer__";

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);
            context.HttpContext.Items[_timerKey] = Stopwatch.StartNew();
        }

        public override void OnActionExecuted(ActionExecutedContext actionExecutedContext)
        {
            base.OnActionExecuted(actionExecutedContext);
            var stopWatch = actionExecutedContext.HttpContext.Items[_timerKey] as Stopwatch;
            if (stopWatch != null)
            {
                Trace.WriteLine("Time elapsed in Milliseconds: " + stopWatch.ElapsedMilliseconds + "\n", "performanceLogger");
            }
        }
    }
}