using SalesTaxesCalculation.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace SalesTaxesCalculation.UnitTests
{
    public partial class SalesTaxesCalculatorTest
    {
        //private readonly double _importTaxPercentage = 0.15;
        //private readonly double _basicTaxPercentage = 0.2;
        //private readonly string _basicTaxLabel = "BASIC";
        //private readonly string _importTaxLabel = "IMPORT";

        //private readonly double _price1 = 9.99;
        //private readonly double _expectedBasicTax1 = 2.00;
        //private readonly double _expectedImportTax1 = 1.50;

        //private readonly double _price2 = 8.96;
        //private readonly double _expectedBasicTax2 = 1.79;
        //private readonly double _expectedImportTax2 = 1.35;

        //private readonly string _exemptType = "book";
        //private readonly string _notExemptType = "tv";


        private readonly SalesTaxesCalculator _sut;

        public SalesTaxesCalculatorTest()
        {
            //todo use chain of factory for that
            //or we can simply associate a taxconfig to each purchase row based on some condition
            _sut = new SalesTaxesCalculator(
                new TaxesConfiguration(
                    DataGenerator.BasicTaxPercentage,
                    DataGenerator.BasicTaxLabel,
                    DataGenerator.ImportTaxPercentage,
                    DataGenerator.ImportTaxLabel), 
                new BasicTaxExemptTypes(new List<string> { DataGenerator.ExemptType }));        

            /*
             * 
             * 
             * 
            something like

            new List<TaxRule>{
                new TaxRule(p => !ExemptItemTypes.Contains(p.Type), p => p.BasicTax, 0.1),
                new TaxRule(p => p.Imported, p => ImportTax, 0.05)      //percentages injected

            something to make virtual calculation without rounding off and concrete calculation (sum of taxes) with roudnign off


            */

        }

        //todo make all trial with data provided by datagenerator
        [Fact]
        public void Should_AssociateCorrectTaxes_ForEachPurchaseRow()
        {
            var rows = DataGenerator.SamplePurchasesRows();
            var purchase = new Purchase(rows);

            var expectedReceiptRows = DataGenerator.SampleReceiptRows();
            IReceipt expected = new FakeReceipt(expectedReceiptRows);
            var actual = _sut.Compute(purchase);

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
