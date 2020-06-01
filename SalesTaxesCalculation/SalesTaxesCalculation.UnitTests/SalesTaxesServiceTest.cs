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
            //only one file at a time, the oldest on the folder, put on error folder if unprocessable, 
            //put on history folder on process ending

            _purchaseRepositoryMock = new Mock<IRepository<PurchaseContainer>>();
            //IEnumerable<Purchase>

            _taxesCalculatorMock = new Mock<ICalculator<Purchase, IReceipt>>();
            _receiptNotifier = new Mock<INotifier<ReceiptContainer>>();
            //IEnumerable<Receipt>

            _logHandler = new Mock<ILogHandler>();

            //todo make an uow inside to process file batch in transaction
            //todo how to handle file management and transaction?
            _sut = new SalesTaxesService(_purchaseRepositoryMock.Object, _taxesCalculatorMock.Object, _receiptNotifier.Object);
        }

        [Fact]
        public async Task Should_CalculateAndNotifyAllReceiptsCorrectly()
        {
            var samplePurchases = GetPurchaseContainer();
            var sampleReceipts = GetReceiptContainer();
            _purchaseRepositoryMock.Setup(i => i.GetPurchases()).ReturnsAsync(samplePurchases).Verifiable();
            _taxesCalculatorMock.Setup(i => i.Compute(It.Is<Purchase>(p => p.Equals(samplePurchases.List[0]))))
                .Returns(sampleReceipts.List[0]).Verifiable();
            _taxesCalculatorMock.Setup(i => i.Compute(It.Is<Purchase>(p => p.Equals(samplePurchases.List[1]))))
                .Returns(sampleReceipts.List[1]).Verifiable();

            await _sut.ProcessPurchases();
            
            _receiptNotifier.Verify(i => i.Notify(It.Is<ReceiptContainer>(p => p.Equals(sampleReceipts))), Times.Once);
            _purchaseRepositoryMock.Verify(i => i.Restore(It.IsAny<PurchaseContainer>()), Times.Never);
        }

        [Fact]
        public async Task Should_LogNoDataFound_OnRepositoyWithoutData()
        {
            var containerWithNoPurchases = new PurchaseContainer(new List<Purchase>());
            _purchaseRepositoryMock.Setup(i => i.GetPurchases()).ReturnsAsync(containerWithNoPurchases).Verifiable();            

            await _sut.ProcessPurchases();

            _taxesCalculatorMock.Verify(i => i.Compute(It.IsAny<Purchase>()), Times.Never);
            _receiptNotifier.Verify(i => i.Notify(It.IsAny<ReceiptContainer>()), Times.Never);
            _purchaseRepositoryMock.Verify(i => i.Restore(It.IsAny<PurchaseContainer>()), Times.Never);
        }

        [Fact]
        public void Should_Throws_IfRepositoryFailToProvideData()
        {
            var expectedErrMessage = "error on getting data";
            var containerWithNoPurchases = new PurchaseContainer(new List<Purchase>());
            _purchaseRepositoryMock.Setup(i => i.GetPurchases()).ThrowsAsync(new Exception(expectedErrMessage)).Verifiable();

            Assert.ThrowsAsync<Exception>(async () => await _sut.ProcessPurchases());

            _logHandler.Verify(i => i.LogError(expectedErrMessage), Times.Once);
            _taxesCalculatorMock.Verify(i => i.Compute(It.IsAny<Purchase>()), Times.Never);
            _receiptNotifier.Verify(i => i.Notify(It.IsAny<ReceiptContainer>()), Times.Never);
            _purchaseRepositoryMock.Verify(i => i.Restore(It.IsAny<PurchaseContainer>()), Times.Never);
        }

        [Fact]
        public void Should_Throws_IfCalculatorFailToProcessPurchases()
        {
            var expectedErrMsg = "error on process data";
            var samplePurchases = GetPurchaseContainer();
            var sampleReceipts = GetReceiptContainer();
            _purchaseRepositoryMock.Setup(i => i.GetPurchases()).ReturnsAsync(samplePurchases).Verifiable();
            _taxesCalculatorMock.Setup(i => i.Compute(It.Is<Purchase>(p => p.Equals(samplePurchases.List[0]))))
                .Returns(sampleReceipts.List[0]);
            _taxesCalculatorMock.Setup(i => i.Compute(It.Is<Purchase>(p => p.Equals(samplePurchases.List[1]))))
                .Throws(new Exception(expectedErrMsg));

            Assert.ThrowsAsync<Exception>(async () => await _sut.ProcessPurchases());

            _logHandler.Verify(i => i.LogError(expectedErrMsg), Times.Once);
            _taxesCalculatorMock.Verify(i => i.Compute(It.IsAny<Purchase>()), Times.Never);
            _receiptNotifier.Verify(i => i.Notify(It.IsAny<ReceiptContainer>()), Times.Never);
            _purchaseRepositoryMock.Verify(i => i.Restore(It.Is<PurchaseContainer>(p => p.Equals(samplePurchases))), Times.Once);
        }

        [Fact]
        public void Should_Throws_IfNotifierFailToProcessAnyReceipt()
        {
            var expectedErrMsg = "error on notify data";
            var samplePurchases = GetPurchaseContainer();
            var sampleReceipts = GetReceiptContainer();
            _purchaseRepositoryMock.Setup(i => i.GetPurchases()).ReturnsAsync(samplePurchases).Verifiable();
            _taxesCalculatorMock.Setup(i => i.Compute(It.Is<Purchase>(p => p.Equals(samplePurchases.List[0]))))
                .Returns(sampleReceipts.List[0]).Verifiable();
            _taxesCalculatorMock.Setup(i => i.Compute(It.Is<Purchase>(p => p.Equals(samplePurchases.List[1]))))
                .Returns(sampleReceipts.List[1]).Verifiable();
            _receiptNotifier.Setup(i => i.Notify(It.IsAny<ReceiptContainer>())).ThrowsAsync(new Exception(expectedErrMsg));

            Assert.ThrowsAsync<Exception>(async () => await _sut.ProcessPurchases());

            _logHandler.Verify(i => i.LogError(expectedErrMsg), Times.Once);
            _purchaseRepositoryMock.Verify(i => i.Restore(It.Is<PurchaseContainer>(p => p.Equals(samplePurchases))), Times.Once);
        }

        public PurchaseContainer GetPurchaseContainer()
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

        public ReceiptContainer GetReceiptContainer()
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
