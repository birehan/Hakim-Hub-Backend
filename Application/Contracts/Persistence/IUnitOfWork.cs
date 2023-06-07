namespace Application.Contracts.Persistence
{
    public interface IUnitOfWork : IDisposable
    {
        ISpecialityRepository SpecialityRepository { get; }

        Task<int> Save();

    }
}