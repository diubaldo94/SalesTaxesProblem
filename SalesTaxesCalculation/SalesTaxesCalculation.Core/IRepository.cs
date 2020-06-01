using System.Threading.Tasks;

namespace SalesTaxesCalculation.Core
{
    public interface IRepository<T>
    {
        Task<PurchaseContainer> GetPurchases();
        Task Restore(PurchaseContainer purchaseContainer);
        Task Backup(PurchaseContainer purchases);
    }
}