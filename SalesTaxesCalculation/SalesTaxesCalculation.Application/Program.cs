using System.Collections.Generic;
using System.Configuration;
using System.Reflection.Emit;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SalesTaxesCalculation.Core;
using SalesTaxesCalculation.UnitTests;

namespace SalesTaxesCalculation.Application
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((ctx, services) =>
                {
                    services.Configure<FileSystemConfiguration>(ctx.Configuration.GetSection("FileSystemConfiguration"));
                    //services.Configure<TaxesConfiguration>(ctx.Configuration.GetSection("TaxesConfiguration"));

                    services.AddSingleton(typeof(IRepository<>), typeof(FileRepository<>));
                    services.AddSingleton<IMapper<PurchaseContainer>, CustomMapper>();
                    services.AddSingleton<ILogHandler, LogHandler>();
                    services.AddSingleton<INotifier<ReceiptContainer>, ReceiptNotifier>();
                    services.AddSingleton<ITaxesProvider, TaxesProvider>();
                    services.AddSingleton<SalesTaxesService>();
                    services.AddSingleton<ICalculator<Purchase, IReceipt>, SalesTaxesCalculator>();
                    services.AddHostedService<Service>();
                });
    }
}
