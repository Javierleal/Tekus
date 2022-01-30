using Domain.Services;
using System;
using System.Collections.Generic;

namespace API.DTOs.Services
{
    public class ServiceInfoDTO
    {
        public int CurrentPage { get; set; }
        public int pageSize { get; set; }
        public int TotalPage { get; set; }

        public List<Service> Services { get; set; }
    }
}