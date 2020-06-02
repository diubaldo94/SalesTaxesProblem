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
}