namespace Application.Contracts.Persistence
{
    public interface IUnitOfWork : IDisposable
    {
        ISpecialityRepository SpecialityRepository { get; }
        IDoctorAvailabilityRepository DoctorAvailabilityRepository {get;}
        IAddressRepository AddressRepository {get;}
        IInstitutionProfileRepository InstitutionProfileRepository {get;}
        IPhotoRepository PhotoRepository {get;}
        Task<int> Save();

    }
}