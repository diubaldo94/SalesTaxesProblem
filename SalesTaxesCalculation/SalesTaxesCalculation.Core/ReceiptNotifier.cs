using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using SalesTaxesCalculation.Core;

namespace SalesTaxesCalculation.UnitTests
{
    public class ReceiptNotifier : INotifier<ReceiptContainer>
    {
        private ILogHandler _logHandler;
        private ICommand _command;
        private IMessageGenerator<ReceiptContainer> _messageGenerator;

        public ReceiptNotifier(ILogHandler logHandler, ICommand command, IMessageGenerator<ReceiptContainer> messageGenerator)
        {
            _logHandler = logHandler;
            _command = command;
            _messageGenerator = messageGenerator;
        }

        public async Task Notify(ReceiptContainer receipts)
        {
            var message = _messageGenerator.Generate(receipts);
            _logHandler.LogInfo(message);
            _ = _command.Launch(message);
        }
    }
}