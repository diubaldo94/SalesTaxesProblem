using System.Threading.Tasks;

namespace SalesTaxesCalculation.Core
{
    public interface ITaxesProvider
    {
        Task<TaxesConfiguration> GetTaxes();
    }
}