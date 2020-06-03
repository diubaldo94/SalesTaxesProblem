using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesTaxesCalculation.Core
{
    public class SalesTaxesCalculator : ICalculator<Purchase, IReceipt>
    {
        //private TaxesConfiguration _taxesConfiguration;
        //private readonly IList<BaseTaxRule<PurchaseRow>> _taxRules;
        private readonly ITaxesProvider _taxesProvider;

        public SalesTaxesCalculator(ITaxesProvider taxesProvider)
        {
            //_taxesConfiguration = taxesConfiguration;
            ////todo what about to make a factroy of rules?
            //_taxRules = new List<BaseTaxRule<PurchaseRow>>
            //{
            //    new BasicTaxRule(_taxesConfiguration.BasicTaxPercentage, _taxesConfiguration.BasicTaxLabel, _taxesConfiguration.BasicTaxExemptTypes),
            //    new ImportTaxRule(_taxesConfiguration.ImportTaxPercentage, _taxesConfiguration.ImportTaxLabel)
            //};
            _taxesProvider = taxesProvider;
        }

        public async Task<IReceipt> Compute(Purchase purchase)
        {
            var taxRules = await _taxesProvider.GetTaxes();
            var receiptRows = new List<IReceiptRow>();
            foreach(var row in purchase.Rows)
            {
                receiptRows.Add(new ReceiptRow(row, taxRules.Where(i => i.ApplyTax(row)).Select(i => i.TaxToApply).ToList()));
            }
            return new Receipt(receiptRows);
        }
    }
}