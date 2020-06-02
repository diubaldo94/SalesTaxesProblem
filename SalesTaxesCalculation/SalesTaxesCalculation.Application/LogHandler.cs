using SalesTaxesCalculation.Core;
using System;

namespace SalesTaxesCalculation.Application
{
    public class LogHandler : ILogHandler
    {
        public void LogError(string errMessage)
        {
            Console.WriteLine(errMessage);
        }

        public void LogInfo(string logMessage)
        {
            Console.WriteLine(logMessage);
        }
    }


}
