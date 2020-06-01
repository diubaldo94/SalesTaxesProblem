using System.Collections.Generic;

namespace SalesTaxesCalculation.Core
{
    public class Receipt : IReceipt
    {

        public Receipt(IList<IReceiptRow> list)
        {
            ReceiptRows = list;
        }

        public IList<IReceiptRow> ReceiptRows { get; }
        public double TaxesAmount()
        {
            throw new System.NotImplementedException();
        }

        public double TotalAmount()
        {
            throw new System.NotImplementedException();
        }
    }

    public class ReceiptRow : IReceiptRow
    {
        public PurchaseRow PurchaseInfo { get; }

        public ReceiptRow(PurchaseRow purchaseRow1)
        {
            PurchaseInfo = purchaseRow1;
        }

        public double TaxesAmount()
        {
            throw new System.NotImplementedException();
        }

        public double TotalAmount()
        {
            throw new System.NotImplementedException();
        }
    }
}