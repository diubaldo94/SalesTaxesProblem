using System.Collections.Generic;

namespace SalesTaxesCalculation.Core
{
    public class ReceiptContainer
    {
        public ReceiptContainer(IList<IReceipt> receipts)
        {
            List = receipts;
        }

        public IList<IReceipt> List { get; }
    }
}