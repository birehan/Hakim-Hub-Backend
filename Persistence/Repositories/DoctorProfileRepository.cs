using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Contracts.Persistence;
using Application.Features.Specialities.DTOs;
using Domain;
using Microsoft.EntityFrameworkCore;

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

        public async Task<List<DoctorProfile>> GetDoctorProfileByGender(string gender)
        {
            var doctorProfiles = await _dbContext.DoctorProfiles.Where(d => d.Gender.ToString() == gender).ToListAsync();
            return doctorProfiles;
        }

        public async Task<List<DoctorProfile>> GetDoctorProfileByCareerStartTime(DateTime careerStartTime)
        {
            var doctorProfiles = await _dbContext.DoctorProfiles.Where(d => d.CareerStartTime == careerStartTime).ToListAsync();
            return doctorProfiles;
        }

        public async Task<List<DoctorProfile>> GetDoctorProfileBySpecialityAndGender(string speciality, string gender)
        {
            var doctorProfiles = await _dbContext.DoctorProfiles.Where(d => d.Specialities.Any(s => s.Name == speciality) && d.Gender.ToString() == gender).ToListAsync();
            return doctorProfiles;
        }


    }
}