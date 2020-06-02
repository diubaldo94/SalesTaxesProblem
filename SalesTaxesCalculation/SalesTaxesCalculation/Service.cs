using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using SalesTaxesCalculation.Core;

namespace SalesTaxesCalculation
{
    public class Service : IHostedService
    {
        private readonly SalesTaxesService _taxesService;

        public Service(SalesTaxesService taxesService)
        {
            _taxesService = taxesService;
        }
        public async Task StartAsync(CancellationToken cancellationToken)
        {
            await _taxesService.ProcessPurchases();
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            //todo to analyze
            return Task.CompletedTask;
        }
    }
}
