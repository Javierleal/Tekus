using Domain.ProviderDetails;

namespace Infrastructure.Data.Repositories
{
    public class ProviderDetailRepository : RepositoryBase<ProviderDetail> , IProviderDetailRepository
    {
        public ProviderDetailRepository(EFContext dbContext) : base(dbContext)
        {

        }
    }
}