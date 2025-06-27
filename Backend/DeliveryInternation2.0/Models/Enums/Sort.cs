using System.Text.Json.Serialization;

namespace DeliveryInternation2._0.Models.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum Sort
    {
        NameAsc,
        NameDesc,
        PriceAsc,
        PriceDesc,
        RatingAsc,
        RatingDesc
    }
}
