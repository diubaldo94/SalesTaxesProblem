using System.Threading.Tasks;

namespace SalesTaxesCalculation.Core
{
    public interface IRepository<T>
    {
        Task<T> GetData();
    }
}