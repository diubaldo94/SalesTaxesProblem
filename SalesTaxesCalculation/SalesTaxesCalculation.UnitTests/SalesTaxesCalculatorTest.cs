using Moq;
using SalesTaxesCalculation.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;
using System.Threading.Tasks;

namespace SalesTaxesCalculation.UnitTests
{
    public partial class SalesTaxesCalculatorTest
    {
        private readonly Mock<ITaxesProvider> _taxesProvider;
        private readonly SalesTaxesCalculator _sut;

        public SalesTaxesCalculatorTest()
        {
            _taxesProvider = new Mock<ITaxesProvider>();
            _sut = new SalesTaxesCalculator(_taxesProvider.Object);       
        }

        [Fact]
        public async Task Should_AssociateCorrectTaxes_ForEachPurchaseRow()
        {
            _taxesProvider.Setup(i => i.GetTaxes()).ReturnsAsync(
                new List<BaseTaxRule<PurchaseRow>>
                {
                    new BasicTaxRule(DataGenerator.BasicTaxPercentage, DataGenerator.BasicTaxLabel, 
                        new BasicTaxExemptTypes(new List<string> { DataGenerator.ExemptType })),
                    new ImportTaxRule(DataGenerator.ImportTaxPercentage, DataGenerator.ImportTaxLabel)
                });
            var rows = DataGenerator.SamplePurchasesRows();
            var purchase = new Purchase(rows);

            var expectedReceiptRows = DataGenerator.SampleReceiptRows();
            IReceipt expected = new FakeReceipt(expectedReceiptRows);
            var actual = await _sut.Compute(purchase);

            double expectedTaxes = expected.TaxesAmount();
            double actualTaxes = actual.TaxesAmount();
            double expectedTotal = expected.TotalAmount();
            double actualTotal = actual.TotalAmount();

            Assert.Equal(purchase.Rows.Count, actual.ReceiptRows.Count);

            Assert.Equal(expectedTaxes, actualTaxes);
            Assert.Equal(expectedTotal, actualTotal);
        }

    }
}
