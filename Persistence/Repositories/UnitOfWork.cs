
using Application.Contracts.Persistence;

namespace Persistence.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly HakimHubDbContext _context;


        private ISpecialityRepository _specialityRepository;
        private IDoctorAvailabilityRepository _doctorAvailabilityRepository;
        private IAddressRepository _addressRepository;
        private IInstitutionProfileRepository _institutionProfileRepository;


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
        public IAddressRepository AddressRepository
        {
            get
            {
                return _addressRepository = new AddressRepository(_context);
            }
        }

        public IInstitutionProfileRepository InstitutionProfileRepository
        {
            get
            {
                return _institutionProfileRepository = new InstitutionProfileRepository(_context);
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