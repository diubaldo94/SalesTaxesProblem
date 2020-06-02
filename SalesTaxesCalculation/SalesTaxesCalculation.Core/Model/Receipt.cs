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
        private readonly IList<Tax> _appliedTaxes;
        public PurchaseRow PurchaseInfo { get; }

        public ReceiptRow(PurchaseRow purchaseRow, IList<Tax> appliedTaxes)
        {
            PurchaseInfo = purchaseRow;
            _appliedTaxes = appliedTaxes;
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