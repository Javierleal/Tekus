using Domain.ProviderServices;

namespace Infrastructure.Data.Repositories
{
    public class ProviderServiceRepository : RepositoryBase<ProviderService> , IProviderServiceRepository
    {
        public ProviderServiceRepository(EFContext dbContext) : base(dbContext)
        {

        }
    }
}