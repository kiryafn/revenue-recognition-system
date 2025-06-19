using System.Text.Json;
using RevenueRecognition.Application.Services.Interfaces;

namespace RevenueRecognition.Application.Services;

public class ExchangeRateProvider(HttpClient http) : IExchangeRateProvider
{
    public async Task<decimal> GetRateAsync(string fromCurrency, string toCurrency, CancellationToken ct = default)
    {
        if (fromCurrency.Equals(toCurrency, StringComparison.OrdinalIgnoreCase))
            return 1m;

        var url = $"latest?base={fromCurrency}&symbols={toCurrency}";
        using var res = await http.GetAsync(url, ct);
        res.EnsureSuccessStatusCode();

        using var doc = await JsonDocument.ParseAsync(
            await res.Content.ReadAsStreamAsync(ct), cancellationToken: ct);

        if (doc.RootElement.GetProperty("rates")
                .TryGetProperty(toCurrency, out var rateElem) &&
            rateElem.TryGetDecimal(out var rate))
        {
            return rate;
        }

        throw new InvalidOperationException($"Cannot parse rate {fromCurrency}->{toCurrency}");
    }
}