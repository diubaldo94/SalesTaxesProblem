﻿using System.Text;
using SalesTaxesCalculation.Core;

namespace SalesTaxesCalculation.UnitTests
{
    public class ReceiptMessageGenerator : IMessageGenerator<ReceiptContainer>
    {
        public string Generate(ReceiptContainer receipts)
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.AppendLine("INPUT");
            stringBuilder.AppendLine();
            //todo to refactor composition of input and output (duplicate code?)
            for (int i = 0; i < receipts.List.Count; i++)
            {
                stringBuilder.AppendLine($"Input {i + 1}:");
                foreach (var row in receipts.List[i].ReceiptRows)
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