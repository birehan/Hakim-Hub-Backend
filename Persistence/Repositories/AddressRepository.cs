using Application.Contracts.Persistence;
using Domain;

namespace Persistence.Repositories
{
    public class AddressRepository : GenericRepository<Address>, IAddressRepository
    {

        private readonly HakimHubDbContext _dbContext;

        public AddressRepository(HakimHubDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

    }
}