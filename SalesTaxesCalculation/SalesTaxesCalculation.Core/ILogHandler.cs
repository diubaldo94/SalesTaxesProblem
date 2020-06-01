namespace SalesTaxesCalculation.Core
{
    public interface ILogHandler
    {
        void LogError(string errMessage);
        void LogInfo(string logMessage);
    }
}