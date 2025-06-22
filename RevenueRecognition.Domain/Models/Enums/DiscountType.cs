using System.Text.Json.Serialization;

namespace RevenueRecognition.Domain.Models.Enums;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum DiscountType
{
    Upfront = 0,
    Subscription = 1,
    Both = 2
}