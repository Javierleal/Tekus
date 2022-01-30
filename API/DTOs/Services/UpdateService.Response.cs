using Domain.Services;
using System;

namespace API.DTOs.Services
{
    public class UpdateServiceInfoDTO
    {
        public string Message { get; set; }

        public bool Success { get; set; }

        public Service Service { get; set; }
    }
}