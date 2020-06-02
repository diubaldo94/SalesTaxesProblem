using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace SalesTaxesCalculation.Dto
{
    public class PurchaseContainerDto
    {
        [JsonProperty("purchases")]
        public IEnumerable<PurchaseDto> PurchaseList { get; set; }
    }

    public class PurchaseDto
    {
        [JsonProperty("items")]
        public IEnumerable<PurchaseRowDto> Rows { get; set; }
    }

    public class PurchaseRowDto
    {
        [JsonProperty("quantity")]
        public int Quantity { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("productType")]
        public string ProductType { get; set; }

        [JsonProperty("imported")]
        public bool Imported { get; set; }

        [JsonProperty("priceBeforeTaxes")]
        public double PriceBeforeTaxes { get; set; }
    }
}
