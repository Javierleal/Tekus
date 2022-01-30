using Domain.Services;

namespace API.DTOs.Services
{
    public class AddServiceInfoDTO
    {
        public string Message { get; set; }

        public bool Success { get; set; }

        public Service Service { get; set; }
    }
}