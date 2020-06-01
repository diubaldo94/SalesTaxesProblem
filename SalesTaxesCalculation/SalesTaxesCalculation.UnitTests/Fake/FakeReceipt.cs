using SalesTaxesCalculation.Core;
using System.Collections.Generic;
using System.Linq;

namespace SalesTaxesCalculation.UnitTests
{
    public class FakeReceipt : IReceipt
    {
        public IList<IReceiptRow> ReceiptRows { get; }

        public FakeReceipt(IList<IReceiptRow> receiptRows)
        {
            ReceiptRows = receiptRows;
        }

        public double TaxesAmount() => ReceiptRows.Sum(i => i.TaxesAmount());
        public double TotalAmount() => ReceiptRows.Sum(i => i.TotalAmount());
    }
    
}
