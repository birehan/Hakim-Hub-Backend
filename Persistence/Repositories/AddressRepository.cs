using Application.Contracts.Persistence;
using Domain;
<<<<<<< HEAD
<<<<<<< HEAD
using Microsoft.EntityFrameworkCore;
=======
>>>>>>> 4db4375 (fix(institution): changes some attributes from institution)
=======
using Microsoft.EntityFrameworkCore;
>>>>>>> 2e3d14f (feat(crud-biruk): add endpoints for address and InstitutionProfile)

namespace Persistence.Repositories
{
    public class AddressRepository : GenericRepository<Address>, IAddressRepository
    {

        private readonly HakimHubDbContext _dbContext;

        public AddressRepository(HakimHubDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

<<<<<<< HEAD
        public async Task<List<Address>> GetAllPopulated()
        {
            return await _dbContext.Set<Address>()
                .Include(x => x.Institution)
                .AsNoTracking().ToListAsync();
        }

        public async Task<Address> GetPopulated(Guid id)
        {
            return await _dbContext.Set<Address>()
                .Include(x => x.Institution)
                .FirstOrDefaultAsync(b => b.Id == id);
        }

=======
>>>>>>> 4db4375 (fix(institution): changes some attributes from institution)
    }
}