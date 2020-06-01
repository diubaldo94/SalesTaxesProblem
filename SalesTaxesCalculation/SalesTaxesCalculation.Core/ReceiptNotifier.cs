using System;
using System.Text;
using SalesTaxesCalculation.Core;

namespace SalesTaxesCalculation.UnitTests
{
    public class ReceiptNotifier
    {
        private ILogHandler _logHandler;

        public ReceiptNotifier(ILogHandler logHandler)
        {
            _logHandler = logHandler;
        }

        public void Notify(ReceiptContainer receipts)
        {
            _logHandler.LogInfo(GenerateMessage(receipts));
        }

        private string GenerateMessage(ReceiptContainer receipts)
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.AppendLine("INPUT");
            stringBuilder.AppendLine();
            //todo to refactor composition of input and output (duplicate code?)
            for(int i = 0; i < receipts.List.Count; i++)
            {
                stringBuilder.AppendLine($"Input {i + 1}:");
                foreach(var row in receipts.List[i].ReceiptRows)
                {
                    stringBuilder.AppendLine($"{row.PurchaseInfo.Quantity} ");
                    if (row.PurchaseInfo.Imported)
                        stringBuilder.Append($"imported ");
                    stringBuilder.Append($"{row.PurchaseInfo.Item.Name} at {row.PurchaseInfo.Item.PriceBeforeTaxes}");
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
                    stringBuilder.AppendLine($"{row.PurchaseInfo.Quantity} ");
                    if (row.PurchaseInfo.Imported)
                        stringBuilder.Append($"imported ");
                    stringBuilder.Append($"{row.PurchaseInfo.Item.Name}: {row.TotalAmount()}");
                }
                stringBuilder.AppendLine($"Sales Taxes: {receipts.List[i].TaxesAmount()}");
                stringBuilder.AppendLine($"Total: {receipts.List[i].TotalAmount()}");
            }
            return stringBuilder.ToString();
        }
    }
}