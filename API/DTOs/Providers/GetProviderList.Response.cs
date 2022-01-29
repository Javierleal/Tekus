using System;

namespace API.DTOs.Users
{
    public class ProviderInfoDTO
    {
        public int Id { get; set; }
        public string NIT { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }
    }
}