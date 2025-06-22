using Microsoft.Extensions.DependencyInjection;
using RevenueRecognition.Application.Services;
using RevenueRecognition.Application.Services.Interfaces;

namespace RevenueRecognition.Application;

public static class ApplicationServicesExtension
{
    public static void RegisterApplicationServices(this IServiceCollection app)
    {
        app.AddScoped<IIndividualClientService, IndividualClientService>();
        app.AddScoped<ICompanyClientService, CompanyClientService>();
        app.AddScoped<ISoftwareProductService, SoftwareProductService>();
        app.AddScoped<IDiscountService, DiscountService>();
        app.AddScoped<IPaymentService, PaymentService>();
        app.AddScoped<IUpfrontContractService, UpfrontContractService>();
        app.AddScoped<IRevenueService, RevenueService>();
        app.AddScoped<IAuthService, AuthService>();
        app.AddHttpClient<IExchangeRateProvider, ExchangeRateProvider>(client => {
                client.BaseAddress = new Uri("https://api.exchangerate.host/");
            });
    }
}