using System.Threading.Tasks;

namespace SalesTaxesCalculation.Core
{
    public interface ILogHandler
    {
        void LogError(string errMessage);
        void LogInfo(string logMessage);
    }

    public interface ICommand
    {
        Task Launch(string message);
    }

    public interface IMessageGenerator<T>
    {
        string Generate(T input);
    }

}