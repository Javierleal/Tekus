using Domain.Providers;
using System;

namespace API.DTOs.Users
{
    public class AddProviderInfoDTO
    {
        public string Message { get; set; }

        public bool Success { get; set; }

        public Provider Provider { get; set; }
    }
}