using Domain.Providers;
using System.Collections.Generic;

namespace API.DTOs.Providers
{
    public class ProviderInfoDTO
    {
        public int CurrentPage { get; set; }
        public int pageSize { get; set; }
        public int TotalPage { get; set; }

        public List<Provider> Providers { get; set; }
    }
}