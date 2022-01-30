using Domain.Interfaces;

namespace Domain.Services
{
    public interface IServiceRepository : IAsyncRepository<Service>
    {
    }
}