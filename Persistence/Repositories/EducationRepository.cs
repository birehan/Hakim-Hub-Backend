using Application.Contracts.Persistence;
using Domain;

namespace Persistence.Repositories;

public class EducationRepository : GenericRepository<Education>, IEducationRepository
{

    private readonly HakimHubDbContext _dbContext;

    public EducationRepository(HakimHubDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }
}
