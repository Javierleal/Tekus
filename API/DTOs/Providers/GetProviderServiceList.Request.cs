namespace API.DTOs.Providers
{
    public class GetProviderServiceRequest
    {
        public string Search { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
    }
}