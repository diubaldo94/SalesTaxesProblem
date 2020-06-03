using System.Threading.Tasks;

namespace SalesTaxesCalculation.Core
{
    public interface ICalculator<T1, T2>
    {
        Task<IReceipt> Compute(Purchase purchase);
    }
}