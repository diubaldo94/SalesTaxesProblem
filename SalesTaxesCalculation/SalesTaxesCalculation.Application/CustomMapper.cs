using System;
using System.Linq;
using System.Linq.Expressions;
using Newtonsoft.Json;
using SalesTaxesCalculation.Application.Dto;
using SalesTaxesCalculation.Application.Exception;
using SalesTaxesCalculation.Core;

namespace SalesTaxesCalculation.Application
{
    public interface IMapper<T>
    {
        T Map(string input);
    }

    public class CustomMapper : IMapper<PurchaseContainer>
    {
        public PurchaseContainer Map(string input)
        {
            PurchaseContainerDto dto;
            try
            {
                dto = JsonConvert.DeserializeObject<PurchaseContainerDto>(input);
            }
            catch (Exception e)
            {
                throw new MapperException(e);
            }

            var purchaseList = dto.PurchaseList.Select(
                i => new Purchase(
                    i.Rows.Select(p => new PurchaseRow(new Item(p.Name, p.ProductType, p.PriceBeforeTaxes), p.Quantity, p.Imported))
                        .ToList()));
            return new PurchaseContainer(purchaseList.ToList());
        }
    }
}
