using Domain.Services;

namespace Infrastructure.Data.Repositories
{
    public class ServiceRepository : RepositoryBase<Service> , IServiceRepository
    {
        public ServiceRepository(EFContext dbContext) : base(dbContext)
        {

        }
    }
}