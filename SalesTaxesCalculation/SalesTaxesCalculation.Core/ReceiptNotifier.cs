using System;
using System.Text;
using System.Threading.Tasks;
using SalesTaxesCalculation.Core;

namespace SalesTaxesCalculation.UnitTests
{
    public class ReceiptNotifier : INotifier<ReceiptContainer>
    {
        private ILogHandler _logHandler;

        public ReceiptNotifier(ILogHandler logHandler)
        {
            _logHandler = logHandler;
        }

        public async Task Notify(ReceiptContainer receipts)
        {
            string logMessage = GenerateMessage(receipts);
            _logHandler.LogInfo(logMessage);
        }

        private string GenerateMessage(ReceiptContainer receipts)
        {
            //todo fix decimals show (always two)
            var stringBuilder = new StringBuilder();
            stringBuilder.AppendLine("INPUT");
            stringBuilder.AppendLine();
            //todo to refactor composition of input and output (duplicate code?)
            for(int i = 0; i < receipts.List.Count; i++)
            {
                stringBuilder.AppendLine($"Input {i + 1}:");
                foreach(var row in receipts.List[i].ReceiptRows)
                {
                    stringBuilder.Append($"{row.PurchaseInfo.Quantity} ");
                    if (row.PurchaseInfo.Imported)
                        stringBuilder.Append($"imported ");
                    stringBuilder.Append($"{row.PurchaseInfo.Item.Name} at {string.Format("{0:0.00}", row.PurchaseInfo.Item.PriceBeforeTaxes)}");
                    stringBuilder.AppendLine();
                }
                stringBuilder.AppendLine();
            }
            stringBuilder.AppendLine("OUTPUT");
            stringBuilder.AppendLine();
            for (int i = 0; i < receipts.List.Count; i++)
            {
                stringBuilder.AppendLine($"Output {i + 1}:");
                foreach (var row in receipts.List[i].ReceiptRows)
                {
                    stringBuilder.Append($"{row.PurchaseInfo.Quantity} ");
                    if (row.PurchaseInfo.Imported)
                        stringBuilder.Append($"imported ");
                    stringBuilder.Append($"{row.PurchaseInfo.Item.Name}: {string.Format("{0:0.00}", row.TotalAmount())}");
                    stringBuilder.AppendLine();
                }
                stringBuilder.AppendLine($"Sales Taxes: {string.Format("{0:0.00}", receipts.List[i].TaxesAmount())}");
                stringBuilder.AppendLine($"Total: {string.Format("{0:0.00}", receipts.List[i].TotalAmount())}");
                stringBuilder.AppendLine();
            }
            return stringBuilder.ToString().Trim();
        }
    }
}