using Application.Contracts.Persistence;
using Domain;

namespace Persistence.Repositories
{
    public class PhotoRepository : GenericRepository<Photo>, IPhotoRepository
    {

        private readonly HakimHubDbContext _dbContext;

        public PhotoRepository(HakimHubDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

    }
}