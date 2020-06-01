using System;
using System.Collections.Generic;
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
            var purchases = await _repo.GetPurchases();

            try
            {
                var generatedReceipts = new List<IReceipt>();
                foreach(var purchase in purchases.List)
                {
                    var receipt = _calculator.Compute(purchase);
                    generatedReceipts.Add(receipt);
                }
                await _notifier.Notify(new ReceiptContainer(generatedReceipts));
                _ = _repo.Backup(purchases);
            }
            catch(Exception e)
            {
                _logHandler.LogError(e.Message);
                _ = _repo.Restore(purchases);
            }
        }
    }
}