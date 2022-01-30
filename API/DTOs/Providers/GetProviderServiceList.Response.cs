using Domain.Providers;
using System.Collections.Generic;

namespace API.DTOs.Providers
{
    public class ProviderServiceInfoDTO
    {
        public int CurrentPage { get; set; }
        public int pageSize { get; set; }
        public int TotalPage { get; set; }

        public List<ProviderServiceDetail> ProviderServices { get; set; }
    }
}