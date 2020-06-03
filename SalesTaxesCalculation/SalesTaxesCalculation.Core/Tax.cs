using System;

namespace SalesTaxesCalculation.Core
{
    public abstract class Tax
    {
        private string _taxLabel;
        protected double _percentage;

        public Tax(string importTaxLabel, double percentage)
        {
            _taxLabel = importTaxLabel;
            _percentage = percentage;
        }
        public virtual double CalculateAmount(Params param) => param.PriceBeforeTaxes * _percentage;

        public class Params
        {
            public double PriceBeforeTaxes { get; }
            public Params(double priceBeforeTaxes)
            {
                PriceBeforeTaxes = priceBeforeTaxes;
            }
        }
    }

    public class SimpleTax : Tax
    {
        public SimpleTax(string importTaxLabel, double percentage) : base(importTaxLabel, percentage) { }
    }

    public class RoundedTax : Tax
    {
        public RoundedTax(string importTaxLabel, double percentage) : base(importTaxLabel, percentage) { }

        public override double CalculateAmount(Params param)
        {
            double v = param.PriceBeforeTaxes * _percentage;
            double v1 = Math.Round(v * 20);
            return v1 / 20;
        }
    }
}