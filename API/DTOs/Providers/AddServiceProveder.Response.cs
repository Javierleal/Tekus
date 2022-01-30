using API.Services.Providers;

namespace API.DTOs.Providers
{
    public class AddServiceProviderInfoDTO
    {
        public string Message { get; set; }

        public bool Success { get; set; }

        public Domain.ProviderServices.ProviderService ServiceProvider { get; set; }
    }
}