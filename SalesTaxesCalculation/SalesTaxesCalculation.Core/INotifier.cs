using System.Threading.Tasks;

namespace SalesTaxesCalculation.Core
{
    public interface INotifier<T>
    {
        Task Notify(ReceiptContainer receiptContainer);
    }
}