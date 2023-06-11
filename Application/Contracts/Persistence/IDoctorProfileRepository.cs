using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Features.Specialities.DTOs;
using Domain;

namespace Application.Contracts.Persistence
{
    public interface IDoctorProfileRepository : IGenericRepository<DoctorProfile>
    {
        public Task<List<DoctorProfile>> GetDoctorProfileBySpecialityId(Guid SpecialityId);
        public Task<List<DoctorProfile>> GetDoctorProfileByInstitutionId(Guid InstitutionId);
        public Task<List<DoctorProfile>> GetDoctorProfileByGender(string gender);
        public Task<List<DoctorProfile>> GetDoctorProfileByCareerStartTime(DateTime careerStartTime);
        public Task<List<DoctorProfile>> GetDoctorProfileBySpecialityAndGender(string speciality, string gender);
    }
}