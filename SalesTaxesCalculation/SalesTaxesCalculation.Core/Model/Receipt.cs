using System.Collections.Generic;
using System.Linq;

namespace SalesTaxesCalculation.Core
{
    public class Receipt : IReceipt
    {

        public Receipt(IList<IReceiptRow> list)
        {
            ReceiptRows = list;
        }

        public IList<IReceiptRow> ReceiptRows { get; }
        public double TaxesAmount() => ReceiptRows.Sum(i => i.TaxesAmount());
        
        public double TotalAmount() => ReceiptRows.Sum(i => i.TotalAmount());
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
            double v = TaxesForPiece();
            return PurchaseInfo.Quantity * v;
        }

        public double TotalAmount() => PurchaseInfo.Quantity *
            (PurchaseInfo.Item.PriceBeforeTaxes + TaxesForPiece());

        private double TaxesForPiece() => _appliedTaxes.Sum(i => i.CalculateAmount(new Tax.Params(PurchaseInfo.Item.PriceBeforeTaxes)));

    }
}