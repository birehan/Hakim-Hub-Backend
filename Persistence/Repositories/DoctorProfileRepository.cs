using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Application.Contracts.Persistence;
using Application.Features.Specialities.DTOs;
using Domain;
using Microsoft.EntityFrameworkCore;
using Npgsql;
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

        public async Task<DoctorProfile> GetDoctor(Guid Id)
        {
            var doctorProfile = await _dbContext.DoctorProfiles
         .Where(d => d.Id == Id)
         .Include(d=>d.Photo)
         .Include(d=>d.Institutions)
         .Include(d => d.Specialities)
         .Include(d => d.Educations)
         .Include(d=>d.Experiences)
        
         .FirstOrDefaultAsync();
            return doctorProfile;
        }
        public async Task<List<DoctorProfile>> GetDoctorProfileBySpecialityId(Guid specialityId)
        {
            var doctorProfiles = await _dbContext.DoctorProfiles.Where(d => d.Specialities.Any(s => s.Id == specialityId)).Include(d=>d.Specialities)
            .Include(d=>d.Photo)
        .ToListAsync();
            return doctorProfiles;
        }

        public async Task<List<DoctorProfile>> GetDoctorProfileByInstitutionId(Guid InstitutionId)
        {
            var doctorProfiles = await _dbContext.DoctorProfiles.Where(d => d.Institutions.Any(e => e.Id == InstitutionId))
            .Include(d=>d.Institutions)
            .Include(d=>d.Photo)
            .ToListAsync();
            return doctorProfiles;
        }

        public async Task<List<DoctorProfile>> GetDoctorProfileByGender(GenderType gender)
        {
            var doctorProfiles = await _dbContext.DoctorProfiles.Where(d => d.Gender == gender)
            .Include(d=>d.Photo)
            .ToListAsync();
            return doctorProfiles;
        }

        public async Task<List<DoctorProfile>> GetDoctorProfileByCareerStartTime(DateTime careerStartTime)
        {
            var doctorProfiles = await _dbContext.DoctorProfiles.Where(d => d.CareerStartTime == careerStartTime)
            .Include(d=>d.Experiences)
            .Include(d=>d.Photo)
            .ToListAsync();
            return doctorProfiles;
        }

        public async Task<List<DoctorProfile>> GetDoctorProfileBySpecialityIdAndGender(Guid specialityId, GenderType gender)
        {
            var doctorProfiles = await _dbContext.DoctorProfiles.Where(d => d.Specialities.Any(s => s.Id == specialityId) && d.Gender == gender)
            .Include(d=>d.Specialities)
            .Include(d=>d.Photo)
            .ToListAsync();
            return doctorProfiles;
        }



        public async Task<List<DoctorProfile>> FilterDoctors(Guid institutionId, string? specialityName, DateTime? careerStartTime, string? educationInstitutionName)
        {
            var query = _dbContext.DoctorProfiles.AsQueryable();
            query = query.Where(d=>d.Institutions.Any(i=>i.Id==institutionId));
            
            // Filter by Institution ID
            if (institutionId != Guid.Empty)
            {
                query = query.Where(d => d.Institutions.Any(i => i.Id == institutionId))
                .Include(d => d.Specialities).Include(e => e.Educations);
            }

            if (!string.IsNullOrEmpty(specialityName))
            {

                query = query.Where(d => d.Specialities.Any(s => s.Name == specialityName));
                Console.WriteLine(query);
            }

            if (!string.IsNullOrEmpty(educationInstitutionName))
            {
                query = query.Where(d => d.Educations.Any(e => e.EducationInstitution == educationInstitutionName));
            }

            if (careerStartTime.HasValue)
            {
                query = query.Where(d => d.CareerStartTime == careerStartTime);
            }

            var filteredDoctors = await query.ToListAsync();

            return filteredDoctors;


        }


    }
}