using Domain.Providers;
using System;

namespace API.DTOs.Providers
{
    public class UpdateServiceProviderInfoDTO
    {
        public string Message { get; set; }

        public bool Success { get; set; }

        public Domain.ProviderServices.ProviderService ServiceProvider { get; set; }
    }
}