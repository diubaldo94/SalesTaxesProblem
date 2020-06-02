using System;

namespace SalesTaxesCalculation.Core
{
    public abstract class BaseTaxRule<T>
    {
        public Tax TaxToApply { get; }

        public BaseTaxRule(double percentage, string taxLabel)
        {
            TaxToApply = new RoundedTax(taxLabel, percentage);
        }

        public abstract bool ApplyTax(T row);
    }

    public class BasicTaxRule : BaseTaxRule<PurchaseRow>
    {
        private BasicTaxExemptTypes _exemptTypes;

        public BasicTaxRule(double percentage, string taxLabel, BasicTaxExemptTypes exemptTypes) : base(percentage, taxLabel)
        {
            _exemptTypes = exemptTypes;
        }

        public override bool ApplyTax(PurchaseRow row) => !_exemptTypes.List.Contains(row.Item.ProductType);
    }

    public class ImportTaxRule : BaseTaxRule<PurchaseRow>
    {
        public ImportTaxRule(double percentage, string taxLabel) : base(percentage, taxLabel)
        {
        }

        public override bool ApplyTax(PurchaseRow row) => row.Imported;
    }
}