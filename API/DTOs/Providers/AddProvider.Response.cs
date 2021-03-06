using Domain.Providers;

namespace API.DTOs.Providers
{
    public class AddProviderInfoDTO
    {
        public string Message { get; set; }

        public bool Success { get; set; }

        public Provider Provider { get; set; }
    }
}