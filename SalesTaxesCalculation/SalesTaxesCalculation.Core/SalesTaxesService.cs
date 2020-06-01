using System;
using System.Threading.Tasks;

namespace SalesTaxesCalculation.Core
{
    public class SalesTaxesService
    {
        private readonly IRepository<PurchaseContainer> object1;
        private readonly ICalculator<Purchase, IReceipt> object2;
        private readonly INotifier<ReceiptContainer> object3;

        public SalesTaxesService(IRepository<PurchaseContainer> object1, ICalculator<Purchase, IReceipt> object2, INotifier<ReceiptContainer> object3)
        {
            this.object1 = object1;
            this.object2 = object2;
            this.object3 = object3;
        }

        public Task ProcessPurchases()
        {
            throw new NotImplementedException();
        }
    }
}