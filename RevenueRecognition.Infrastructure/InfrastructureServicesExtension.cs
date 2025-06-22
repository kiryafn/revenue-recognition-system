using Microsoft.Extensions.DependencyInjection;
using RevenueRecognition.Application.Repositories;
using RevenueRecognition.Infrastructure.Repositories;

namespace RevenueRecognition.Infrastructure;

public static class InfrastructureServicesExtension
{
    public static void RegisterInfraServices(this IServiceCollection app)
    {
        app.AddScoped<IIndividualClientRepository, IndividualClientRepository>();
        app.AddScoped<ICompanyClientRepository, CompanyClientRepository>();
        app.AddScoped<ISoftwareProductRepository, SoftwareProductRepository>();
        app.AddScoped<IDiscountRepository, DiscountRepository>();
        app.AddScoped<IPaymentRepository, PaymentRepository>();
        app.AddScoped<IUpfrontContractRepository, UpfrontContractRepository>();
        app.AddScoped<IClientRepository, ClientRepository>();
        app.AddScoped<IUserRepository, UserRepository>();
        app.AddDbContext<AppDbContext>();
    }
}