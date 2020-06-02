namespace SalesTaxesCalculation.Core
{
    public class Item
    {
        public string Name { get; }
        public string ProductType { get; }
        public double PriceBeforeTaxes { get; }

        public Item(string name, string productType, double price)
        {
            Name = name;
            ProductType = productType;
            PriceBeforeTaxes = price;
        }
    }
}