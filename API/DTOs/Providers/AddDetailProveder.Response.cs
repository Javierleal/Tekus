using Domain.ProviderDetails;

namespace API.DTOs.Providers
{
    public class AddDetailProviderInfoDTO
    {
        public string Message { get; set; }

        public bool Success { get; set; }

        public ProviderDetail ServiceDetail { get; set; }
    }
}