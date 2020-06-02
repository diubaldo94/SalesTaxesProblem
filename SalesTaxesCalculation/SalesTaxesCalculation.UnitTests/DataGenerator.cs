using SalesTaxesCalculation.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace SalesTaxesCalculation.UnitTests
{
    public class DataGenerator
    {
        public static double ImportTaxPercentage = 0.15;
        public static double BasicTaxPercentage = 0.2;
        public static string BasicTaxLabel = "BASIC";
        public static string ImportTaxLabel = "IMPORT";
                       
        private static readonly double _price1 = 9.99;
        private static readonly double _expectedBasicTax1 = 2.00;
        private static readonly double _expectedImportTax1 = 1.50;
                       
        private static readonly double _price2 = 8.96;
        private static readonly double _expectedBasicTax2 = 1.79;
        private static readonly double _expectedImportTax2 = 1.35;
                       
        public static string ExemptType = "book";
        public static string NotExemptType = "tv";
                       
        private static readonly PurchaseRow rowWithImportBasicTax1 = new PurchaseRow(new Item("i1", NotExemptType, _price1), 3, true);
        private static readonly PurchaseRow rowWithImportTax1 = new PurchaseRow(new Item("i2", ExemptType, _price1), 5, true);
        private static readonly PurchaseRow rowWithBasicTax1 = new PurchaseRow(new Item("i3", NotExemptType, _price1), 33, false);
        private static readonly PurchaseRow rowWithNoTax1 = new PurchaseRow(new Item("i4", ExemptType, _price1), 7, false);
        private static readonly PurchaseRow rowWithImportBasicTax2 = new PurchaseRow(new Item("i5", NotExemptType, _price2), 1, true);
        private static readonly PurchaseRow rowWithImportTax2 = new PurchaseRow(new Item("i6", ExemptType, _price2), 22, true);
        private static readonly PurchaseRow rowWithBasicTax2 = new PurchaseRow(new Item("i7", NotExemptType, _price2), 1, false);
        private static readonly PurchaseRow rowWithNoTax2 = new PurchaseRow(new Item("i8", ExemptType, _price2), 2, false);

        //public static (PurchaseContainer, ReceiptContainer) GetSamplePurchaseContainerWithRelatedReceipt()
        //{
        //    var purchaseRow1 = new PurchaseRow(new Item("book", "", 12.49), 2, false);
        //    var purchaseRow2 = new PurchaseRow(new Item("music CD", "", 14.99), 1, false);
        //    var purchaseRow3 = new PurchaseRow(new Item("chocolate bar", "", 0.85), 1, false);
        //    var purchaseRow4 = new PurchaseRow(new Item("box of chocolates", "", 10), 1, false);
        //    var purchaseRow5 = new PurchaseRow(new Item("bottle of perfume", "", 47.50), 1, false);

        //    var taxesConfigNoImportDuty = new TaxRule(0.1, 0);
        //    var taxesConfigImportDuty5 = new TaxRule(0.1, 0.05);

        //    var purchases = new List<Purchase>()
        //    {
        //        new Purchase(new List<PurchaseRow>{purchaseRow1, purchaseRow2, purchaseRow3}),
        //        new Purchase(new List<PurchaseRow>{purchaseRow4, purchaseRow5}),
        //    };
        //    var receipts = new List<IReceipt>()
        //    {
        //        new Receipt(
        //            new List<ReceiptRow>
        //            {
        //                new ReceiptRow(purchaseRow1),
        //                new ReceiptRow(purchaseRow2),
        //                new ReceiptRow(purchaseRow3),
        //            }
        //        ),
        //        new Receipt(
        //            new List<ReceiptRow>
        //            {
        //                new ReceiptRow(purchaseRow4),
        //                new ReceiptRow(purchaseRow5),
        //            }
        //        ),
        //    };
        //    return (new PurchaseContainer(purchases), new ReceiptContainer(receipts));
        //}

        public static IList<PurchaseRow> SamplePurchasesRows()
        {
            
            return new List<PurchaseRow>
            {
                rowWithImportBasicTax1, rowWithImportTax1, rowWithBasicTax1, rowWithNoTax1,
                rowWithImportBasicTax2, rowWithImportTax2, rowWithBasicTax2, rowWithNoTax2,
            };
        }

        public static IList<IReceiptRow> SampleReceiptRows()
        {
            var expectedReceiptRows = new List<IReceiptRow>()
            {
                new FakeReceiptRow(rowWithImportBasicTax1,
                    new List<FakeTax>{GetBasicTaxWithAmount(_expectedBasicTax1), GetImportTaxWithAmount(_expectedImportTax1)}

                ),
                new FakeReceiptRow(rowWithImportTax1,
                    new List<FakeTax>{GetImportTaxWithAmount(_expectedImportTax1)}

                ),
                new FakeReceiptRow(rowWithBasicTax1,
                    new List<FakeTax>{GetBasicTaxWithAmount(_expectedBasicTax1)}

                ),
                new FakeReceiptRow(rowWithImportBasicTax2,
                    new List<FakeTax>{GetBasicTaxWithAmount(_expectedBasicTax2), GetImportTaxWithAmount(_expectedImportTax2)}

                ),
                new FakeReceiptRow(rowWithImportTax2,
                    new List<FakeTax>{GetImportTaxWithAmount(_expectedImportTax2)}

                ),
                new FakeReceiptRow(rowWithBasicTax2,
                    new List<FakeTax>{GetBasicTaxWithAmount(_expectedBasicTax2)}

                ),
                new FakeReceiptRow(rowWithNoTax1),
                new FakeReceiptRow(rowWithNoTax2),
            };
            return expectedReceiptRows;
        }

        public static IList<IReceipt> GetReceipts()
        {
            var rows = SampleReceiptRows();
            return new List<IReceipt>
            {
                new Receipt(new List<IReceiptRow>{ rows[0], rows[1], rows[2] }),
                new Receipt(new List<IReceiptRow>{ rows[3], rows[4], rows[5] }),
                new Receipt(new List<IReceiptRow>{ rows[6], rows[7] }),
            };
        }

        public static string GetMessage()
        {
            return @"INPUT:

Input 1:
2 book at 12.49
1 music CD at 14.99
1 chocolate bar at 0.85

Input 2:
1 imported box of chocolates at 10.00
1 imported bottle of perfume at 47.50

Input 3:
1 imported bottle of perfume at 27.99
1 bottle of perfume at 18.99
1 packet of headache pills at 9.75
3 box of imported chocolates at 11.25

OUTPUT

Output 1:
2 book: 24.98
1 music CD: 16.49
1 chocolate bar: 0.85
Sales Taxes: 1.50
Total: 42.32

Output 2:
1 imported box of chocolates: 10.50
1 imported bottle of perfume: 54.65
Sales Taxes: 7.65
Total: 65.15

Output 3:
1 imported bottle of perfume: 32.19
1 bottle of perfume: 20.89
1 packet of headache pills: 9.75
3 imported box of chocolates: 35.55
Sales Taxes: 7.90
Total: 98.38";
        }
        
        private static FakeTax GetImportTaxWithAmount(double amount) => new FakeTax
        {
            Label = ImportTaxLabel,
            Amount = amount
        };
        private static FakeTax GetBasicTaxWithAmount(double amount) => new FakeTax
        {
            Label = BasicTaxLabel,
            Amount = amount
        };
    }

    
}
