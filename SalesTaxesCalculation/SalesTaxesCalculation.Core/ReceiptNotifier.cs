using System;
using SalesTaxesCalculation.Core;

namespace SalesTaxesCalculation.UnitTests
{
    public class ReceiptNotifier
    {
        private object @object;

        public ReceiptNotifier(object @object)
        {
            this.@object = @object;
        }

        public void Notify(ReceiptContainer @object)
        {
            throw new NotImplementedException();
        }
    }
}