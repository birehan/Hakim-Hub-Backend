
using Application.Contracts.Persistence;

namespace Persistence.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly HakimHubDbContext _context;


        private ISpecialityRepository _specialityRepository;
        private IDoctorAvailabilityRepository _doctorAvailabilityRepository;
        private IExperienceRepository _experienceRepository;
        private IServiceRepository _serviceRepository;



        public UnitOfWork(HakimHubDbContext context)
        {
            _context = context;
        }

        public ISpecialityRepository SpecialityRepository
        {
            get
            {
                return _specialityRepository ??= new SpecialityRepository(_context);
            }
        }
        public IDoctorAvailabilityRepository DoctorAvailabilityRepository
        {
            get
            {
                return _doctorAvailabilityRepository = new DoctorAvailabilityRepository(_context);
            }
        }
        public IExperienceRepository ExperienceRepository
        {
            get
            {
                return _experienceRepository ??= new ExperienceRepository(_context);
            }
        }
        public IServiceRepository ServiceRepository
        {
            get
            {
                return _serviceRepository ??= new ServiceRepository(_context);
            }
        }

        public void Dispose()
        {
            _context.Dispose();
            GC.SuppressFinalize(this);
        }

        public async Task<int> Save()
        {
            return await _context.SaveChangesAsync();
        }
    }
}