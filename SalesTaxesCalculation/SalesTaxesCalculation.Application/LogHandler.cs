using SalesTaxesCalculation.Core;
using System;

namespace SalesTaxesCalculation.Application
{
    public class LogHandler : ILogHandler
    {
        public void LogError(string errMessage)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(errMessage);
        }

        public void LogInfo(string logMessage)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(logMessage);
        }
    }


}
