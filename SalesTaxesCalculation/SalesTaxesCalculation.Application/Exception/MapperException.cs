using System;

namespace SalesTaxesCalculation.Application.Exception
{
    public class MapperException : Exception
    {
        public MapperException(Exception e) : base(e.Message, e) { }
    }
}
