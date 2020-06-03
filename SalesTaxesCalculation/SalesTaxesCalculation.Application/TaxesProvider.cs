using System.Collections.Generic;
using System.Threading.Tasks;
using SalesTaxesCalculation.Core;

namespace SalesTaxesCalculation.Application
{
    public class TaxesProvider : ITaxesProvider
    {
        public async Task<IList<BaseTaxRule<PurchaseRow>>> GetTaxes()
        {
            //simple repo, info could be injected or polled by db or file o other
            return new List<BaseTaxRule<PurchaseRow>>
            {
                new BasicTaxRule(0.1, "BASIC", new BasicTaxExemptTypes(new List<string>{ "books","food","medical products"})),
                new ImportTaxRule(0.05, "IMPORT")
            };
        }
    }


}
