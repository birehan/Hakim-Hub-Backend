using Application.Contracts.Persistence;
using Domain;

namespace Persistence.Repositories
{
    public class UserRepository : GenericRepository<AppUser>, IUserRepository
    {

        private readonly HakimHubDbContext _dbContext;

        public UserRepository(HakimHubDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

    }
}