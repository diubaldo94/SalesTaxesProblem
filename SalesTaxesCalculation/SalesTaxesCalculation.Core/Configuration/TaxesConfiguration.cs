namespace SalesTaxesCalculation.Core
{
    public class TaxesConfiguration
    {
        public double BasicTaxPercentage { get; }
        public double ImportTaxPercentage { get; }
        public string BasicTaxLabel { get; }
        public string ImportTaxLabel { get; }


        public TaxesConfiguration(double basicTaxPercentage, string basicTaxLabel, double importTaxPercentage, string importTaxLabel)
        {
            BasicTaxPercentage = basicTaxPercentage;
            BasicTaxLabel = basicTaxLabel;
            ImportTaxPercentage = importTaxPercentage;
            ImportTaxLabel = importTaxLabel;
        }
    }
}