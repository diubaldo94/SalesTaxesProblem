using System;
using System.Collections.Generic;
using System.Text;

namespace SalesTaxesCalculation.UnitTests
{
    class ReceiptNotifierTest
    {
        private readonly ILogHandler _logHandlerMock;
        private readonly ReceiptNotifier _sut;

        public ReceiptNotifierTest()
        {
            _logHandlerMock = new Mock<ILogHandler>();
            _sut = new ReceiptNotifier(_logHandlerMock.Object);
        }


    }
}
