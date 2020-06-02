using SalesTaxesCalculation.Core;
using System.Collections.Generic;
using System.Linq;

namespace SalesTaxesCalculation.UnitTests
{
    public  class FakeReceiptRow : IReceiptRow
    {
        public PurchaseRow PurchaseInfo { get; }
        private readonly IList<FakeTax> _taxList;

        public FakeReceiptRow(PurchaseRow rowWithNoTax)
        {
            PurchaseInfo = rowWithNoTax;
            _taxList = new List<FakeTax>();
        }

        public FakeReceiptRow(PurchaseRow rowWithImportBasicTax1, IList<FakeTax> list)
        {
            PurchaseInfo = rowWithImportBasicTax1;
            _taxList = list;
        }
        public double TaxesAmount() => _taxList.Sum(i => i.Amount) * PurchaseInfo.Quantity;
        public double TotalAmount() => (_taxList.Sum(i => i.Amount) + PurchaseInfo.Item.PriceBeforeTaxes) * PurchaseInfo.Quantity;
    }    

    public class FakeTax
    {
        public double Amount { get; set; }
        public string Label { get; set; }
    }
}
