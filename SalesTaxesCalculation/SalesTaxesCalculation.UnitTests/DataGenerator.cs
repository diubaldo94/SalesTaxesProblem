using System;
using System.Collections.Generic;
using System.Text;

namespace SalesTaxesCalculation.UnitTests
{
    public class DataGenerator
    {
        public static (PurchaseContainer, ReceiptContainer) GetSamplePurchaseContainerWithRelatedReceipt()
        {
            var purchaseRow1 = new PurchaseRow(new Item("book", 12.49), 2, false);
            var purchaseRow2 = new PurchaseRow(new Item("music CD", 14.99), 1, false);
            var purchaseRow3 = new PurchaseRow(new Item("chocolate bar", 0.85), 1, false);
            var purchaseRow4 = new PurchaseRow(new Item("box of chocolates", 10), 1, false);
            var purchaseRow5 = new PurchaseRow(new Item("bottle of perfume", 47.50), 1, false);

            var taxesConfigNoImportDuty = new TaxesConfig(0.1, 0);
            var taxesConfigImportDuty5 = new TaxesConfig(0.1, 0.05);

            var purchases = new List<Purchase>()
            {
                new Purchase(new List<PurchaseRow>{purchaseRow1, purchaseRow2, purchaseRow3}),
                new Purchase(new List<PurchaseRow>{purchaseRow4, purchaseRow5}),
            };
            var receipts = new List<Receipt>()
            {
                new Receipt(
                    new List<ReceiptRow>
                    {
                        new ReceiptRow(purchaseRow1),
                        new ReceiptRow(purchaseRow2),
                        new ReceiptRow(purchaseRow3),
                    }
                ),
                new Receipt(
                    new List<ReceiptRow>
                    {
                        new ReceiptRow(purchaseRow4),
                        new ReceiptRow(purchaseRow5),
                    }
                ),
            };
            return (new PurchaseContainer(purchases), new ReceiptContainer(receipts));
        }
    }
}
