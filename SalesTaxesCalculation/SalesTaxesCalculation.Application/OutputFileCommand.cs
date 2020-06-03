using System;
using System.IO;
using System.Threading.Tasks;
using SalesTaxesCalculation.Core;

namespace SalesTaxesCalculation.Application
{
    public class OutputFileCommand : ICommand
    {
        private readonly OutputConfiguration _configuration;
        public OutputFileCommand(OutputConfiguration configuration)
        {
            _configuration = configuration;
        }
        public Task Launch(string message)
        {
            return File.WriteAllTextAsync(Path.Combine(_configuration.Path, $"{DateTime.Now:yyyyMMddHHmmss}_Output.txt)"), message);
        }
    }
}
