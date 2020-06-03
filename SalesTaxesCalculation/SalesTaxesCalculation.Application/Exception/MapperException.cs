using System;

namespace SalesTaxesCalculation.Application
{
    public class MapperException : Exception
    {
        public MapperException(Exception e) : base(e.Message, e) { }
    }
}
