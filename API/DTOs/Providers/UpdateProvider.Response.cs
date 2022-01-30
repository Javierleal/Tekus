using Domain.Providers;
using System;

namespace API.DTOs.Providers
{
    public class UpdateProviderInfoDTO
    {
        public string Message { get; set; }

        public bool Success { get; set; }

        public Provider Provider { get; set; }
    }
}