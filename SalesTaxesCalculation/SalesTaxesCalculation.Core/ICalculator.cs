using System.Threading.Tasks;

namespace SalesTaxesCalculation.Core
{
    public interface ICalculator<T1, T2>
    {
        //todo better an interface or something to accept various ouptu string format
        Task<IReceipt> Compute(Purchase purchase);
    }
}