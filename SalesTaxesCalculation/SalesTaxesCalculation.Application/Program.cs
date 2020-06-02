using System.Collections.Generic;
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
                    var config = new ConfigurationBuilder();

                    //services.Configure<AppSettings>(ctx.Configuration.GetSection("ExemptTypes"));

                    //todo to inject
                    //services.Configure(new RepositoryConfig(ctx.Configuration.GetSection("RepositoryConfig")));

                    services.AddSingleton(typeof(IRepository<>), typeof(FileRepository<>));
                    services.AddSingleton<IMapper<PurchaseContainer>, CustomMapper>();
                    services.AddSingleton<ILogHandler, LogHandler>();
                    services.AddSingleton<INotifier<ReceiptContainer>, ReceiptNotifier>();
                    services.AddSingleton<SalesTaxesService>();
                    //todo to inject from config
                    services.AddSingleton<ICalculator<Purchase, IReceipt>>(
                        new SalesTaxesCalculator(new TaxesConfiguration(0.1, "BASIC", 0.05, "IMPORT"),
                        new BasicTaxExemptTypes(new List<string> { "food", "medical products", "books" })));
                    services.AddHostedService<Service>();
                });
    }
}
