using System.Text.Json.Serialization;

namespace RevenueRecognition.Domain.Models.Enums;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum ContractStatus
    {
        Pending,   
        Signed,
        Cancelled   
    }