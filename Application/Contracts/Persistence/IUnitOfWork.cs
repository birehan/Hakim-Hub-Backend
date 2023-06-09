namespace Application.Contracts.Persistence
{
    public interface IUnitOfWork : IDisposable
    {
        ISpecialityRepository SpecialityRepository { get; }
        IDoctorAvailabilityRepository DoctorAvailabilityRepository {get;}
        IInstitutionAvailabilityRepository InstitutionAvailabilityRepository{get;}
        Task<int> Save();

    }
}