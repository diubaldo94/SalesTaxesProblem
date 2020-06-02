using Moq;
using SalesTaxesCalculation.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace SalesTaxesCalculation.UnitTests
{
    public class ReceiptNotifierTest
    {
        private readonly Mock<ILogHandler> _logHandlerMock;
        private readonly ReceiptNotifier _sut;

        public ReceiptNotifierTest()
        {
            _logHandlerMock = new Mock<ILogHandler>();

            _sut = new ReceiptNotifier(_logHandlerMock.Object);
        }

        [Fact]
        public async Task Should_ShowOnConsoleResult()
        {
            var receipts = DataGenerator.GetReceipts();
            await _sut.Notify(new ReceiptContainer(receipts));

            var expectedLogMessage = DataGenerator.GetMessage().Trim();
            _logHandlerMock.Verify(i => i.LogInfo(expectedLogMessage), Times.Once);
        }
    }
}
