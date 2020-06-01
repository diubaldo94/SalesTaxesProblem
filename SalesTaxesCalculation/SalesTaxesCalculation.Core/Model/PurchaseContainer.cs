using System.Collections.Generic;

namespace SalesTaxesCalculation.Core
{
    public class PurchaseContainer
    {
        public PurchaseContainer(List<Purchase> list)
        {
            List = list;
        }

        public IList<Purchase> List { get; }
    }
}