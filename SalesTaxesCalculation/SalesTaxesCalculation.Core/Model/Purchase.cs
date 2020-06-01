using System.Collections.Generic;
using SalesTaxesCalculation.UnitTests;

namespace SalesTaxesCalculation.Core
{
    public class Purchase
    {
        public IList<PurchaseRow> Rows { get; }

        public Purchase(IList<PurchaseRow> list)
        {
            Rows = list;
        }
    }

    public class PurchaseRow
    {
        public Item Item { get; }
        public int Quantity { get; }
        public bool Imported { get; }

        public PurchaseRow(Item item, int qty, bool imported)
        {
            Item = item;
            Quantity = qty;
            Imported = imported;
        }

    }
}