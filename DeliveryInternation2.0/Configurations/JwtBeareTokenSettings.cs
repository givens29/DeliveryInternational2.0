namespace DeliveryInternation2._0.Configurations
{
    public class JwtBeareTokenSettings
    {
        public string SecretKey { get; set; }
        public string Audience { get; set; }
        public string Issuer { get; set; }
        public int ExpiryTimeInSeconds { get; set; }

    }
}
