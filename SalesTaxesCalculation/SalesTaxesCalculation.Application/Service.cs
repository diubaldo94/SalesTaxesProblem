using System;
using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using SalesTaxesCalculation.Core;

namespace SalesTaxesCalculation.Application
{
    public class Service : IHostedService
    {
        private readonly SalesTaxesService _taxesService;
        private readonly IHostApplicationLifetime _applicationLifetime;

        public Service(SalesTaxesService taxesService, IHostApplicationLifetime applicationLifetime)
        {
            _taxesService = taxesService;
            _applicationLifetime = applicationLifetime;
        }
        public async Task StartAsync(CancellationToken cancellationToken)
        {
            await _taxesService.ProcessPurchases();
            _applicationLifetime.StopApplication();
        }

        public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
    }
}
