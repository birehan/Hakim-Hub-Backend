using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Contracts.Persistence;
using Application.Features.Specialities.DTOs;
using Domain;
using Microsoft.EntityFrameworkCore;
using static Domain.DoctorProfile;

namespace Persistence.Repositories
{
    public class DoctorProfileRepository : GenericRepository<DoctorProfile>, IDoctorProfileRepository
    {
        private readonly HakimHubDbContext _dbContext;
        public DoctorProfileRepository(HakimHubDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }


        public async Task<List<DoctorProfile>> GetDoctorProfileBySpecialityId(Guid specialityId)
        {
            var doctorProfiles = await _dbContext.DoctorProfiles.Where(d => d.Specialities.Any(s => s.Id == specialityId))
        .ToListAsync();
            return doctorProfiles;
        }

        public async Task<List<DoctorProfile>> GetDoctorProfileByInstitutionId(Guid InstitutionId)
        {
            var doctorProfiles = await _dbContext.DoctorProfiles.Where(d => d.Institutions.Any(e => e.Id == InstitutionId)).ToListAsync();
            return doctorProfiles;
        }

        public async Task<List<DoctorProfile>> GetDoctorProfileByGender(GenderType gender)
        {
            var doctorProfiles = await _dbContext.DoctorProfiles.Where(d => d.Gender == gender).ToListAsync();
            return doctorProfiles;
        }

        public async Task<List<DoctorProfile>> GetDoctorProfileByCareerStartTime(DateTime careerStartTime)
        {
            var doctorProfiles = await _dbContext.DoctorProfiles.Where(d => d.CareerStartTime == careerStartTime).ToListAsync();
            return doctorProfiles;
        }

        public async Task<List<DoctorProfile>> GetDoctorProfileBySpecialityIdAndGender(Guid specialityId, GenderType gender)
        {
            var doctorProfiles = await _dbContext.DoctorProfiles.Where(d => d.Specialities.Any(s => s.Id == specialityId) && d.Gender == gender).ToListAsync();
            return doctorProfiles;
        }

        public async Task<List<DoctorProfile>> FilterDoctors(Guid specialityID, Guid educationId, DateTime careerStartTime, Guid institutionId)
        {
            var query = _dbContext.DoctorProfiles.AsQueryable();

            // Filter by Institution ID
            if (institutionId != Guid.Empty)
            {
                query = query.Where(d => d.Institutions.Any(i => i.Id == institutionId));
            }

            // Filter by other query parameters
            if (specialityID != Guid.Empty || educationId != Guid.Empty || careerStartTime != DateTime.MinValue)
            {
                query = query.Where(d =>
                    (specialityID == Guid.Empty || d.Specialities.Any(s => s.Id == specialityID)) &&
                    (educationId == Guid.Empty || d.Educations.Any(e => e.Id == educationId)) &&
                    (careerStartTime == DateTime.MinValue || d.CareerStartTime == careerStartTime)
                );
            }

            var filteredDoctors = await query.ToListAsync();

            return filteredDoctors;
        }


    }
}