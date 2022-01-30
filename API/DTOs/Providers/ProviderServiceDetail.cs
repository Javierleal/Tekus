namespace API.DTOs.Providers
{
    public class ProviderServiceDetail
    {
        public int Id { get; set; }

        public int IDPrivider { get; set; }

        public string ProviderName { get; set; }

        public int IDService { get; set; }

        public string ServiceName { get; set; }

        public decimal PriceHour { get; set; }

        public string CountryISO { get; set; }

        public string CountryName { get; set; }
    }
}
