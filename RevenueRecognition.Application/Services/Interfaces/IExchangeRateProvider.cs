namespace RevenueRecognition.Application.Services.Interfaces;

public interface IExchangeRateProvider
{
    Task<decimal> GetRateAsync(string fromCurrency, string toCurrency, CancellationToken ct = default);
}