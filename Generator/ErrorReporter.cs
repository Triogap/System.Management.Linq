using System.Diagnostics;
namespace System.Management.Generator;

public class ErrorReporter
{
    public static void Report(string message, Exception? exception = null, bool throwOrBreak = true)
    {
#if DEBUG
        Console.WriteLine($"Exception occured ===> {message}");
        if (throwOrBreak && Debugger.IsAttached)
        {
            Debugger.Break();
        }
        else
#endif
        if (throwOrBreak)
        {
            throw new Exception(message, exception);
        }
    }
}