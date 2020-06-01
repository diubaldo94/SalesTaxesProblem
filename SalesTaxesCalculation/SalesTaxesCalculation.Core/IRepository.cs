using System.Threading.Tasks;

namespace SalesTaxesCalculation.Core
{
    public interface IRepository<T>
    {
        Task<T> GetData();
        Task Restore(T data);
        Task Backup(T data);
    }
}