using Microsoft.Extensions.DependencyInjection;
using RevenueRecognition.Application.Repositories;
using RevenueRecognition.Infrastructure;
using RevenueRecognition.Infrastructure.Repositories;

namespace Trip.Infrastructure;

public static class InfrastructureServicesExtension
{
    public static void RegisterInfraServices(this IServiceCollection app)
    {
        app.AddScoped<IIndividualClientRepository, IndividualClientRepository>();
        app.AddScoped<ICompanyClientRepository, CompanyClientRepository>();
        app.AddDbContext<AppDbContext>();
    }
}