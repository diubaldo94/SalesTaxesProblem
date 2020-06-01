namespace SalesTaxesCalculation.Core
{
    public class TaxesConfiguration
    {
        private double _basicTaxPercentage;
        private double _importTaxPercentage;
        private string _basicTaxLabel;
        private string _importTaxLabel;
        

        public TaxesConfiguration(double basicTaxPercentage, string basicTaxLabel, double importTaxPercentage, string importTaxLabel)
        {
            _basicTaxPercentage = basicTaxPercentage;
            _basicTaxLabel = basicTaxLabel;
            _importTaxPercentage = importTaxPercentage;
            _importTaxLabel = importTaxLabel;
        }
    }
}