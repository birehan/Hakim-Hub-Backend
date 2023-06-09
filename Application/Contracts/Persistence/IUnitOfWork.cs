namespace Application.Contracts.Persistence
{
    public interface IUnitOfWork : IDisposable
    {
        ISpecialityRepository SpecialityRepository { get; }
        IEducationRepository EducationRepository { get; }
        IDoctorAvailabilityRepository DoctorAvailabilityRepository {get;}

        Task<int> Save();

    }
}