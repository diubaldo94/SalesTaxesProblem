using System.Threading.Tasks;
using System.Collections.Generic;

namespace SalesTaxesCalculation.Core
{
    public interface ITaxesProvider
    {
        Task<IList<BaseTaxRule<PurchaseRow>>> GetTaxes();
    }
}