using System;

namespace SalesTaxesCalculation
{
    public class MapperException : Exception
    {
        public MapperException(Exception e) : base(e.Message, e) { }
    }
}
