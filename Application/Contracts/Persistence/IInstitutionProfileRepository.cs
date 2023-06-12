using Domain;

namespace Application.Contracts.Persistence
{
    public interface IInstitutionProfileRepository : IGenericRepository<InstitutionProfile>
    {
<<<<<<< HEAD
<<<<<<< HEAD
        Task<List<InstitutionProfile>> GetAllPopulated();
        Task<InstitutionProfile> GetPopulated(Guid id);
=======
>>>>>>> 2e3d14f (feat(crud-biruk): add endpoints for address and InstitutionProfile)
        Task<List<InstitutionProfile>> GetByYears(int years);
        Task<List<InstitutionProfile>> GetByService(Guid id);

        Task<List<InstitutionProfile>> Search(string serviceName, int operationYears, bool openStatus);
        Task<List<InstitutionProfile>> Search(string Name);

<<<<<<< HEAD
=======
>>>>>>> 4db4375 (fix(institution): changes some attributes from institution)
=======
>>>>>>> 2e3d14f (feat(crud-biruk): add endpoints for address and InstitutionProfile)

    }
}