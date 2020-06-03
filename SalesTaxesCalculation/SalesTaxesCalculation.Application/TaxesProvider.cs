using System.Collections.Generic;
using System.Threading.Tasks;
using SalesTaxesCalculation.Core;

namespace SalesTaxesCalculation.Application
{
    public class TaxesProvider : ITaxesProvider
    {
        public async Task<IList<BaseTaxRule<PurchaseRow>>> GetTaxes()
        {
            //_taxesConfiguration = taxesConfiguration;
            ////todo what about to make a factroy of rules?
            return new List<BaseTaxRule<PurchaseRow>>
            {
                new BasicTaxRule(0.1, "BASIC", new BasicTaxExemptTypes(new List<string>{ "books","food","medical products"})),
                new ImportTaxRule(0.05, "IMPORT")
            };
        }
    }


}
