using Application.Contracts.Persistence;
using Domain;

namespace Persistence.Repositories
{
    public class InstitutionProfileRepository : GenericRepository<InstitutionProfile>, IInstitutionProfileRepository
    {

        private readonly HakimHubDbContext _dbContext;

        public InstitutionProfileRepository(HakimHubDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

    }
}