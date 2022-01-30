using Domain.ProviderDetails;

namespace API.DTOs.Providers
{
    public class UpdateDetailProviderInfoDTO
    {
        public string Message { get; set; }

        public bool Success { get; set; }

        public ProviderDetail DetailProvider { get; set; }
    }
}