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
        public Task<DoctorProfile> GetDoctorProfileDetail(Guid Id);
        public Task<List<DoctorProfile>> GetAllDoctors();

        public Task<List<DoctorProfile>> GetDoctorProfileByInstitutionId(Guid InstitutionId);

        public Task<List<DoctorProfile>> GetDoctorProfileByCareerStartTime(DateTime careerStartTime);
        public Task<List<DoctorProfile>> FilterDoctors(Guid? institutionId, string? specialityName = "", int experienceYears = -1, string? educationInstitutionName = null);
        
    }
}