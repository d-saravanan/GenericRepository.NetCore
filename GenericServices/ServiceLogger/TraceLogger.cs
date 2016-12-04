using System.Diagnostics;

namespace GenericService.ServiceLogger
{
    public class TraceLogger : ILogger
    {
        public void Log(string category, string message, TraceLevel level)
        {
            Trace.WriteLine(message, category);
        }
    }
}
