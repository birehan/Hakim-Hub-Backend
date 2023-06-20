using Domain;

namespace Application.Contracts.Persistence
{
    public interface IInstitutionProfileRepository : IGenericRepository<InstitutionProfile>
    {
        Task<List<InstitutionProfile>> GetAllPopulated();
        Task<InstitutionProfile> GetPopulatedInstitution(Guid id);
        Task<List<InstitutionProfile>> GetByYears(int years);
        Task<List<InstitutionProfile>> GetByService(Guid id);

        Task<List<InstitutionProfile>> Search(ICollection<string> serviceNames, int operationYears, bool openStatus);
        Task<List<InstitutionProfile>> Search(string Name);


    }
}