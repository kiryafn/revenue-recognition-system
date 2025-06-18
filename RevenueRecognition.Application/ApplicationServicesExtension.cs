using Microsoft.Extensions.DependencyInjection;
using RevenueRecognition.Application.Repositories;
using RevenueRecognition.Application.Services;
using RevenueRecognition.Application.Services.Interfaces;

namespace TripApp.Application;

public static class ApplicationServicesExtension
{
    public static void RegisterApplicationServices(this IServiceCollection app)
    {
        app.AddScoped<IIndividualClientService, IndividualClientService>();
        app.AddScoped<ICompanyClientService, CompanyClientService>();
    }
}