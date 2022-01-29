using Domain.Providers;

namespace Infrastructure.Data.Repositories
{
    public class ProviderRepository : RepositoryBase<Provider> , IProviderRepository
    {
        public ProviderRepository(EFContext dbContext) : base(dbContext)
        {

        }
    }
}