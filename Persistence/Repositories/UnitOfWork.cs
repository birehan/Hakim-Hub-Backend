
using Application.Contracts.Persistence;

namespace Persistence.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly HakimHubDbContext _context;


        private ISpecialityRepository _specialityRepository;
        private IDoctorAvailabilityRepository _doctorAvailabilityRepository;
        private IInstitutionAvailabilityRepository _institutionAvailabilityRepository;



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

        public IInstitutionAvailabilityRepository InstitutionAvailabilityRepository
        {
            get
            {
                return _institutionAvailabilityRepository = new InstitutionAvailabilityRepository(_context);
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