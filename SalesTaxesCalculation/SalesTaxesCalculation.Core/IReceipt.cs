using System.Collections.Generic;

namespace SalesTaxesCalculation.Core
{
    public interface IReceipt
    {
        IList<IReceiptRow> ReceiptRows { get; }
        double TaxesAmount();
        double TotalAmount();
    }

    public interface IReceiptRow
    {
        PurchaseRow PurchaseInfo { get; }
        double TaxesAmount();
        double TotalAmount();
    }

    public class Tax
    {
        private string _importTaxLabel;
        private double amount;

        public Tax(string importTaxLabel, double amount)
        {
            _importTaxLabel = importTaxLabel;
            this.amount = amount;
        }

        public int Amount { get; }
    }
}