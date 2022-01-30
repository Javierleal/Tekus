using Domain.ProviderDetails;
using System.Collections.Generic;

namespace API.DTOs.Providers
{
    public class ProviderDetailInfoDTO
    {
        public int CurrentPage { get; set; }
        public int pageSize { get; set; }
        public int TotalPage { get; set; }

        public List<ProviderDetail> ProviderDetails { get; set; }
    }
}