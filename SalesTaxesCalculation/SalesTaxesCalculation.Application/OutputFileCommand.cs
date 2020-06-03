using System;
using System.IO;
using System.Threading.Tasks;
using SalesTaxesCalculation.Core;
using Microsoft.Extensions.Options;

namespace SalesTaxesCalculation.Application
{
    public class OutputFileCommand : ICommand
    {
        private readonly OutputConfiguration _configuration;
        public OutputFileCommand(IOptions<OutputConfiguration> configuration)
        {
            _configuration = configuration.Value;
        }
        public Task Launch(string message)
        {
            return File.WriteAllTextAsync(Path.Combine(_configuration.Path, $"{DateTime.Now:yyyyMMddHHmmss}_Output.txt"), message);
        }
    }
}
