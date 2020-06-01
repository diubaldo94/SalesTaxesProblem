using System.Threading.Tasks;

namespace SalesTaxesCalculation.Core
{
    public interface IRepository<T>
    {
        Task<PurchaseContainer> GetPurchases();
        void Restore(PurchaseContainer purchaseContainer);
    }
}