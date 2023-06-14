using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Features.Specialities.DTOs;
using Domain;
using static Domain.DoctorProfile;

namespace Application.Contracts.Persistence
{
    public interface IDoctorProfileRepository : IGenericRepository<DoctorProfile>
    {
        public Task<DoctorProfile> GetDoctor(Guid Id);
        public Task<List<DoctorProfile>> GetDoctorProfileBySpecialityId(Guid SpecialityId);
        public Task<List<DoctorProfile>> GetDoctorProfileByInstitutionId(Guid InstitutionId);
        public Task<List<DoctorProfile>> GetDoctorProfileByGender(GenderType gender);
        public Task<List<DoctorProfile>> GetDoctorProfileByCareerStartTime(DateTime careerStartTime);
        public Task<List<DoctorProfile>> GetDoctorProfileBySpecialityIdAndGender(Guid specialityId, GenderType gender);
        public Task<List<DoctorProfile>> FilterDoctors(Guid institutionId, string? specialityName = null, DateTime? careerStartTime = null, string? educationInstitutionName = null);
        
    }
}