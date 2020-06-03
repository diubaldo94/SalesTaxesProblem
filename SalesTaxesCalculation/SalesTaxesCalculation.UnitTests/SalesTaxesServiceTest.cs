using Moq;
using SalesTaxesCalculation.Core;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace SalesTaxesCalculation.UnitTests
{
    public class SalesTaxesServiceTest
    {
        private readonly SalesTaxesService _sut;
        private readonly Mock<IRepository<PurchaseContainer>> _purchaseRepositoryMock;
        private readonly Mock<ICalculator<Purchase, IReceipt>> _taxesCalculatorMock;
        private readonly Mock<INotifier<ReceiptContainer>> _receiptNotifier;
        private readonly Mock<ILogHandler> _logHandler;

        public SalesTaxesServiceTest()
        {
            _purchaseRepositoryMock = new Mock<IRepository<PurchaseContainer>>();
            _taxesCalculatorMock = new Mock<ICalculator<Purchase, IReceipt>>();
            _receiptNotifier = new Mock<INotifier<ReceiptContainer>>();
            _logHandler = new Mock<ILogHandler>();
            _sut = new SalesTaxesService(_purchaseRepositoryMock.Object, _taxesCalculatorMock.Object, _receiptNotifier.Object, _logHandler.Object);
        }

        [Fact]
        public async Task Should_CalculateAndNotifyAllReceiptsCorrectly()
        {
            var samplePurchases = GetPurchaseContainer();
            var sampleReceipts = GetReceiptContainer();
            _purchaseRepositoryMock.Setup(i => i.GetData()).ReturnsAsync(samplePurchases).Verifiable();
            _taxesCalculatorMock.Setup(i => i.Compute(It.Is<Purchase>(p => p.Equals(samplePurchases.List[0]))))
                .ReturnsAsync(sampleReceipts.List[0]).Verifiable();
            _taxesCalculatorMock.Setup(i => i.Compute(It.Is<Purchase>(p => p.Equals(samplePurchases.List[1]))))
                .ReturnsAsync(sampleReceipts.List[1]).Verifiable();

            await _sut.ProcessPurchases();
            
            _receiptNotifier.Verify(i => i.Notify(It.Is<ReceiptContainer>(
                p => p.Equals(sampleReceipts)            
            )), Times.Once);
        }

        [Fact]
        public async Task Should_LogNoDataFound_OnRepositoyWithoutData()
        {
            var containerWithNoPurchases = new PurchaseContainer(new List<Purchase>());
            _purchaseRepositoryMock.Setup(i => i.GetData()).ReturnsAsync(containerWithNoPurchases).Verifiable();            

            await _sut.ProcessPurchases();

            _taxesCalculatorMock.Verify(i => i.Compute(It.IsAny<Purchase>()), Times.Never);
            _receiptNotifier.Verify(i => i.Notify(It.IsAny<ReceiptContainer>()), Times.Never);
        }

        [Fact]
        public void Should_Throws_IfRepositoryFailToProvideData()
        {
            var expectedErrMessage = "error on getting data";
            var containerWithNoPurchases = new PurchaseContainer(new List<Purchase>());
            _purchaseRepositoryMock.Setup(i => i.GetData()).ThrowsAsync(new Exception(expectedErrMessage)).Verifiable();

            Assert.ThrowsAsync<Exception>(async () => await _sut.ProcessPurchases());

            _logHandler.Verify(i => i.LogError(It.IsAny<string>()), Times.Once);
            _taxesCalculatorMock.Verify(i => i.Compute(It.IsAny<Purchase>()), Times.Never);
            _receiptNotifier.Verify(i => i.Notify(It.IsAny<ReceiptContainer>()), Times.Never);
        }

        [Fact]
        public void Should_Throws_IfCalculatorFailToProcessPurchases()
        {
            var expectedErrMsg = "error on process data";
            var samplePurchases = GetPurchaseContainer();
            var sampleReceipts = GetReceiptContainer();
            _purchaseRepositoryMock.Setup(i => i.GetData()).ReturnsAsync(samplePurchases).Verifiable();
            _taxesCalculatorMock.Setup(i => i.Compute(It.Is<Purchase>(p => p.Equals(samplePurchases.List[0]))))
                .ReturnsAsync(sampleReceipts.List[0]).Verifiable();
            _taxesCalculatorMock.Setup(i => i.Compute(It.Is<Purchase>(p => p.Equals(samplePurchases.List[1]))))
                .ThrowsAsync(new Exception(expectedErrMsg)).Verifiable();

            Assert.ThrowsAsync<Exception>(async () => await _sut.ProcessPurchases());

            _logHandler.Verify(i => i.LogError(expectedErrMsg), Times.Once);
            _receiptNotifier.Verify(i => i.Notify(It.IsAny<ReceiptContainer>()), Times.Never);
        }

        [Fact]
        public void Should_Throws_IfNotifierFailToProcessAnyReceipt()
        {
            var expectedErrMsg = "error on notify data";
            var samplePurchases = GetPurchaseContainer();
            var sampleReceipts = GetReceiptContainer();
            _purchaseRepositoryMock.Setup(i => i.GetData()).ReturnsAsync(samplePurchases).Verifiable();
            _taxesCalculatorMock.Setup(i => i.Compute(It.Is<Purchase>(p => p.Equals(samplePurchases.List[0]))))
                .ReturnsAsync(sampleReceipts.List[0]).Verifiable();
            _taxesCalculatorMock.Setup(i => i.Compute(It.Is<Purchase>(p => p.Equals(samplePurchases.List[1]))))
                .ReturnsAsync(sampleReceipts.List[1]).Verifiable();
            _receiptNotifier.Setup(i => i.Notify(It.IsAny<ReceiptContainer>())).ThrowsAsync(new Exception(expectedErrMsg)).Verifiable();

            Assert.ThrowsAsync<Exception>(async () => await _sut.ProcessPurchases());

            _logHandler.Verify(i => i.LogError(expectedErrMsg), Times.Once);
        }

        private PurchaseContainer GetPurchaseContainer()
        {
            var rows = DataGenerator.SamplePurchasesRows();
            return new PurchaseContainer(
                new List<Purchase>
                {
                    new Purchase(new List<PurchaseRow>{ rows[0], rows[1] }),
                    new Purchase(new List<PurchaseRow>{ rows[2], rows[3], rows[4] }),
                }
            );
        }

        private ReceiptContainer GetReceiptContainer()
        {
            var rows = DataGenerator.SampleReceiptRows();
            return new ReceiptContainer(
                new List<IReceipt>
                {
                    new FakeReceipt(new List<IReceiptRow>{ rows[0], rows[1] }),
                    new FakeReceipt(new List<IReceiptRow>{ rows[2], rows[3], rows[4] }),
                }
            );
        }
    }
}
