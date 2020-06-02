using System.Collections.Generic;

namespace SalesTaxesCalculation.Core
{
    public class BasicTaxExemptTypes
    {
        public IList<string> List { get; }

        public BasicTaxExemptTypes(IList<string> types)
        {
            List = types;
        }
    }
}