namespace SalesTaxesCalculation.Core
{
    public class SalesTaxesCalculator : ICalculator<Purchase, IReceipt>
    {
        private TaxesConfiguration taxesConfiguration;

        public SalesTaxesCalculator(TaxesConfiguration taxesConfiguration)
        {
            this.taxesConfiguration = taxesConfiguration;
        }

        public IReceipt Compute(Purchase purchase)
        {
            throw new System.NotImplementedException();
        }
    }
}