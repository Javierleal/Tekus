using Domain.Users;
using Infrastructure.Data.RepoNew;

namespace Infrastructure.Data.Repositories
{
    public class UserRepository : RepositoryUser, IUserRepository
    {
        public UserRepository(EFContext dbContext) : base(dbContext)
        {
        }
    }
}