using System.Text.Json.Serialization;

namespace DeliveryInternation2._0.Models.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum Gender
    {
        Male,
        Female
    }
}
