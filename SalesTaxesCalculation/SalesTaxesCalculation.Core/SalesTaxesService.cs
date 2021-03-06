﻿
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
                _logHandler.LogInfo("Looking for purchases...");
                var purchases = await _repo.GetData();

                if (!purchases.List.Any())
                {
                    _logHandler.LogInfo("No purchases found!");
                    return;
                }
                else
                    _logHandler.LogInfo("Purchases found!");


                var generatedReceipts = new List<IReceipt>();
                foreach(var purchase in purchases.List)
                {
                    var receipt = await _calculator.Compute(purchase);
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