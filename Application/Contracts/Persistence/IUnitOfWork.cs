namespace Application.Contracts.Persistence
{
    public interface IUnitOfWork : IDisposable
    {
        ISpecialityRepository SpecialityRepository { get; }
        IDoctorAvailabilityRepository DoctorAvailabilityRepository {get;}
        IExperienceRepository ExperienceRepository { get; }
        IServiceRepository ServiceRepository { get; }


        Task<int> Save();

    }
}