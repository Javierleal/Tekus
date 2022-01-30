namespace API.DTOs.Services
{
    public class GetServiceRequest
    {
        public string Search { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
    }
}