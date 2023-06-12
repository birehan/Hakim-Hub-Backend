<<<<<<< HEAD
<<<<<<< HEAD
=======
>>>>>>> 2e3d14f (feat(crud-biruk): add endpoints for address and InstitutionProfile)
using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;
using Application.Contracts.Persistence;
using Domain;
using Microsoft.EntityFrameworkCore;
<<<<<<< HEAD
=======
using Application.Contracts.Persistence;
using Domain;
>>>>>>> 4db4375 (fix(institution): changes some attributes from institution)
=======
>>>>>>> 2e3d14f (feat(crud-biruk): add endpoints for address and InstitutionProfile)

namespace Persistence.Repositories
{
    public class InstitutionProfileRepository : GenericRepository<InstitutionProfile>, IInstitutionProfileRepository
    {

        private readonly HakimHubDbContext _dbContext;

        public InstitutionProfileRepository(HakimHubDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

<<<<<<< HEAD
<<<<<<< HEAD
        public async Task<List<InstitutionProfile>> GetAllPopulated()
        {
            return await _dbContext.Set<InstitutionProfile>()
                .Include(x => x.Address)
                .Include(x => x.Logo)
                .Include(x => x.Banner).AsNoTracking().ToListAsync();
        }

        public async Task<InstitutionProfile> GetPopulated(Guid id)
        {
            return await _dbContext.Set<InstitutionProfile>()
                .Include(x => x.Address)
                .Include(x => x.Logo)
                .Include(x => x.Banner)
                .Include(x => x.Services)
                .Include(x => x.Photos)
                .Include(x => x.InstitutionAvailability)
                .Include(x => x.Doctors).FirstOrDefaultAsync(b => b.Id == id);
        }
=======
>>>>>>> 2e3d14f (feat(crud-biruk): add endpoints for address and InstitutionProfile)
        public async Task<List<InstitutionProfile>> GetByYears(int years)
        {
            DateTime startDate = DateTime.Today.AddYears(-years);
            return await _dbContext.Set<InstitutionProfile>()
<<<<<<< HEAD
                .Include(x => x.InstitutionAvailability)
=======
                // .Include(x => x.Address)
>>>>>>> 2e3d14f (feat(crud-biruk): add endpoints for address and InstitutionProfile)
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
<<<<<<< HEAD
                .Include(x => x.InstitutionAvailability)
                .Include(x => x.Logo)
                .Include(x => x.Banner);
=======
                .Include(x => x.InstitutionAvailabilities);
>>>>>>> 2e3d14f (feat(crud-biruk): add endpoints for address and InstitutionProfile)

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
<<<<<<< HEAD
                var currentDayOfWeek = (int)currentDate.DayOfWeek + 1; // Adding 1 to match the numbering convention

                var availabilities = await _dbContext.Set<InstitutionAvailability>()
                    .Where(avail => institutionIds.Contains(avail.InstitutionId) &&
                        (avail.StartDay <= avail.EndDay
                            ? (avail.StartDay <= (DayOfWeek)(currentDayOfWeek % 7) && (DayOfWeek)(currentDayOfWeek % 7) <= avail.EndDay)
                            : (avail.StartDay <= (DayOfWeek)(currentDayOfWeek % 7) || (DayOfWeek)(currentDayOfWeek % 7) <= avail.EndDay))
                    )
                    .ToListAsync();


                var profiles = await query.ToListAsync();

                var filteredProfiles = profiles.Where(x => x.InstitutionAvailability != null &&
                    availabilities.Any(a => a.InstitutionId == x.Id)
                    //     (a.TwentyFourHours ||
                    //      (TimeSpan.Parse(a.Opening) <= currentTime && currentTime <= TimeSpan.Parse(a.Closing))))
                    ).ToList();


                return filteredProfiles;
=======
                var availabilities = await _dbContext.Set<InstitutionAvailability>()
                    .Where(avail => institutionIds.Contains(avail.InstitutionId) &&
                        avail.StartDay == currentDate.DayOfWeek.ToString() &&
                        avail.EndDay == currentDate.DayOfWeek.ToString())
                    .ToListAsync();

                query = query.Where(x => x.InstitutionAvailabilities.Any(avail =>
                    availabilities.Any(a => a.InstitutionId == x.Id &&
                        (a.TwentyFourHours ||
                         IsTimeWithinRange(a.Opening, a.Closing, currentTime))
                    )));
>>>>>>> 2e3d14f (feat(crud-biruk): add endpoints for address and InstitutionProfile)
            }

            return await query.ToListAsync();
        }

<<<<<<< HEAD

=======
>>>>>>> 2e3d14f (feat(crud-biruk): add endpoints for address and InstitutionProfile)
        public async Task<List<InstitutionProfile>> Search(string institutionName)
        {
            IQueryable<InstitutionProfile> query = _dbContext.Set<InstitutionProfile>()
                .Include(x => x.Services)
<<<<<<< HEAD
                .Include(x => x.InstitutionAvailability)
                .Include(x => x.Logo)
                .Include(x => x.Banner);
=======
                .Include(x => x.InstitutionAvailabilities);
>>>>>>> 2e3d14f (feat(crud-biruk): add endpoints for address and InstitutionProfile)
        
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
        
        


        
        
<<<<<<< HEAD
=======
>>>>>>> 4db4375 (fix(institution): changes some attributes from institution)
=======
>>>>>>> 2e3d14f (feat(crud-biruk): add endpoints for address and InstitutionProfile)
    }
}