
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SalesTaxesCalculation.Core
{
    public class SalesTaxesService
    {
        private readonly IRepository<PurchaseContainer> _repo;
        private readonly ICalculator<Purchase, IReceipt> _calculator;
        private readonly INotifier<ReceiptContainer> _notifier;
        private readonly ILogHandler _logHandler;

        public SalesTaxesService(IRepository<PurchaseContainer> repo, ICalculator<Purchase, IReceipt> calculator, 
            INotifier<ReceiptContainer> notifier, ILogHandler logHandler)
        {
            _repo = repo;
            _calculator = calculator;
            _notifier = notifier;
            _logHandler = logHandler;
        }

        public async Task ProcessPurchases()
        {
            try
            {
                var purchases = await _repo.GetData();

                //todo to thron or not?
                if (!purchases.List.Any())
                    return;// throw new Exception("No rows on the purchase file");

                var generatedReceipts = new List<IReceipt>();
                foreach(var purchase in purchases.List)
                {
                    var receipt = _calculator.Compute(purchase);
                    generatedReceipts.Add(receipt);
                }
                await _notifier.Notify(new ReceiptContainer(generatedReceipts));
            }
            catch(Exception e)
            {
                _logHandler.LogError(e.Message);
            }
        }

        public void Dispose()
        {
            
        }
    }
}