using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;
using Application.Contracts.Persistence;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories
{
    public class InstitutionProfileRepository : GenericRepository<InstitutionProfile>, IInstitutionProfileRepository
    {

        private readonly HakimHubDbContext _dbContext;

        public InstitutionProfileRepository(HakimHubDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<InstitutionProfile>> GetByYears(int years)
        {
            DateTime startDate = DateTime.Today.AddYears(-years);
            return await _dbContext.Set<InstitutionProfile>()
                // .Include(x => x.Address)
                .Where(x => x.EstablishedOn <= startDate)
                .ToListAsync();
        }

        public async Task<List<InstitutionProfile>> GetByService(Guid ServiceId)
        {
            return await _dbContext.Set<InstitutionProfile>()
                .Include(x => x.Services)
                .Where(x => x.Services.Any(s => s.Id == ServiceId))
                .ToListAsync();
        }

        public async Task<List<InstitutionProfile>> Search(string serviceName, int operationYears, bool openStatus)
{
    var options = new JsonSerializerOptions
    {
        ReferenceHandler = ReferenceHandler.Preserve,
        MaxDepth = 32
    };

    IQueryable<InstitutionProfile> query = _dbContext.Set<InstitutionProfile>()
        .Include(x => x.Services)
            .ThenInclude(s => s.Institutions)
        .Include(x => x.InstitutionAvailability);

    if (!string.IsNullOrEmpty(serviceName))
    {
        query = query.Where(x => x.Services.Any(service => service.ServiceName == serviceName));
    }

    if (operationYears > 0)
    {
        DateTime startDate = DateTime.Today.AddYears(-operationYears);
        query = query.Where(x => x.EstablishedOn <= startDate);
    }

    if (openStatus)
    {
        var currentDate = DateTime.UtcNow.Date;
            var currentTime = DateTime.UtcNow.TimeOfDay;
        
            var institutionIds = await query.Select(x => x.Id).ToListAsync();
            var currentDayOfWeek = currentDate.DayOfWeek;
        
            var availabilities = await _dbContext.Set<InstitutionAvailability>()
                .Where(avail => institutionIds.Contains(avail.InstitutionId) &&
                    avail.StartDay <= currentDayOfWeek && currentDayOfWeek <= avail.EndDay)
                .ToListAsync();
        
            var profiles = await query.ToListAsync();
        
            var filteredProfiles = profiles.Where(x => x.InstitutionAvailability != null &&
                availabilities.Any(a => a.InstitutionId == x.Id &&
                    (a.TwentyFourHours ||
                     (TimeSpan.Parse(a.Opening) <= currentTime && currentTime <= TimeSpan.Parse(a.Closing))))
                ).ToList();

        return filteredProfiles;
    }

    return await query.ToListAsync();
}


        public async Task<List<InstitutionProfile>> Search(string institutionName)
        {
            IQueryable<InstitutionProfile> query = _dbContext.Set<InstitutionProfile>()
                .Include(x => x.Services)
                .Include(x => x.InstitutionAvailability);
        
            if (!string.IsNullOrEmpty(institutionName))
            {
                string searchTerm = institutionName.ToLower();
                query = query.Where(x => x.InstitutionName.ToLower().Contains(searchTerm));
            }
        
            return await query.ToListAsync();
        }



        
        private static bool IsTimeWithinRange(string opening, string closing, TimeSpan currentTime)
        {
            if (TimeSpan.TryParseExact(opening, "hh\\:mm", CultureInfo.InvariantCulture, out var openingTime) &&
                TimeSpan.TryParseExact(closing, "hh\\:mm", CultureInfo.InvariantCulture, out var closingTime))
            {
                return openingTime <= currentTime && closingTime >= currentTime;
            }
            return false;
        }
        
        


        
        
    }
}