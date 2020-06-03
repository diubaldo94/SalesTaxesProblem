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
        private readonly Mock<ICommand> _command1Mock;
        private readonly Mock<IMessageGenerator<ReceiptContainer>> _messageGenerator;
        private readonly ReceiptNotifier _sut;

        public ReceiptNotifierTest()
        {
            _logHandlerMock = new Mock<ILogHandler>();
            _command1Mock = new Mock<ICommand>();
            _messageGenerator = new Mock<IMessageGenerator<ReceiptContainer>>();
            _sut = new ReceiptNotifier(_logHandlerMock.Object, _command1Mock.Object, _messageGenerator.Object);
        }

        [Fact]
        public async Task Should_ShowOnConsoleResult()
        {
            var receipts = DataGenerator.GetReceipts();
            var receiptsContainer = new ReceiptContainer(receipts);

            var expectedLogMessage = DataGenerator.GetMessage().Trim();
            _messageGenerator.Setup(i => i.Generate(receiptsContainer)).Returns(expectedLogMessage).Verifiable();

            await _sut.Notify(receiptsContainer);

            _logHandlerMock.Verify(i => i.LogInfo(expectedLogMessage), Times.Once);
            _command1Mock.Verify(i => i.Launch(expectedLogMessage), Times.Once);
        }
    }
}
