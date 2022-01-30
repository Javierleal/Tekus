namespace API.DTOs.Providers
{
    public class AddServiceProviderRequest
    {

        public int IDService { get; set; }

        public decimal PriceHour { get; set; }

        public string CountryISO { get; set; }
    }
}