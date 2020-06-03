namespace SalesTaxesCalculation.Core
{
    public class TaxesConfiguration
    {
        public BasicTaxExemptTypes BasicTaxExemptTypes { get; set; }
        public double BasicTaxPercentage { get; set; }
        public double ImportTaxPercentage { get; set; }
        public string BasicTaxLabel { get; set; }
        public string ImportTaxLabel { get; set; }


        //public TaxesConfiguration(double basicTaxPercentage, string basicTaxLabel, double importTaxPercentage, 
        //    string importTaxLabel, BasicTaxExemptTypes basicTaxExemptTypes)
        //{
        //    BasicTaxPercentage = basicTaxPercentage;
        //    BasicTaxLabel = basicTaxLabel;
        //    ImportTaxPercentage = importTaxPercentage;
        //    ImportTaxLabel = importTaxLabel;
        //    BasicTaxExemptTypes = basicTaxExemptTypes;
        //}
    }
}