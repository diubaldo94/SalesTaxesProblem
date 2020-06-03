using System;
using System.Collections.Generic;
using System.Linq;

namespace SalesTaxesCalculation.Core
{
    public class ReceiptContainer
    {
        public ReceiptContainer(IList<IReceipt> receipts)
        {
            List = receipts;
        }

        public IList<IReceipt> List { get; }

        public override bool Equals(object other)
        {
            if (this.GetType() != other.GetType())
                return false;

            double amount = List.Sum(i => i.TotalAmount());
            double otherAmount = ((ReceiptContainer)other).List.Sum(i => i.TotalAmount());
            return amount.Equals(otherAmount);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(List);
        }
    }
}