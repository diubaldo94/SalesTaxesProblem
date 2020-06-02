﻿using System.Collections.Generic;
using System.Linq;

namespace SalesTaxesCalculation.Core
{
    public class SalesTaxesCalculator : ICalculator<Purchase, IReceipt>
    {
        private TaxesConfiguration _taxesConfiguration;
        private readonly IList<BaseTaxRule<PurchaseRow>> _taxRules;

        public SalesTaxesCalculator(TaxesConfiguration taxesConfiguration, BasicTaxExemptTypes exemptTypes)
        {
            _taxesConfiguration = taxesConfiguration;
            //todo what about to make a factroy of rules?
            _taxRules = new List<BaseTaxRule<PurchaseRow>>
            {
                new BasicTaxRule(taxesConfiguration.BasicTaxPercentage, taxesConfiguration.BasicTaxLabel, exemptTypes),
                new ImportTaxRule(taxesConfiguration.ImportTaxPercentage, taxesConfiguration.ImportTaxLabel)
            };
        }

        public IReceipt Compute(Purchase purchase)
        {
            var receiptRows = new List<IReceiptRow>();
            foreach(var row in purchase.Rows)
            {
                receiptRows.Add(new ReceiptRow(row, _taxRules.Where(i => i.ApplyTax(row)).Select(i => i.TaxToApply).ToList()));
            }
            return new Receipt(receiptRows);
        }
    }
}