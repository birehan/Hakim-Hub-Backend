using Domain;
using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Contracts.Persistence;
using Moq;

namespace Application.UnitTest.Mocks
{
    public static class MockInstitutionProfileRepository
    {
        public static Mock<IInstitutionProfileRepository> GetInstitutionProfileRepository()
        {
            var institutionProfiles = new List<InstitutionProfile>
            {
                new InstitutionProfile
                {
                    Id = new Guid("3fa85f64-5717-4562-b3fc-2c963f66afa6"),
                    InstitutionName = "Institution 1",
                    BranchName = "Branch 1",
                    Website = "Website 1",
                    PhoneNumber = "Phone 1",
                    Summary = "Summary 1",
                    EstablishedOn = DateTime.Now,
                    Rate = 4.5,
                    AddressId = new Guid("3fa85f64-5717-4562-b3fc-2c963f66afa6"),
                    LogoId = "LogoId 1",
                    BannerId = "BannerId 1"
                },
                new InstitutionProfile
                {
                    Id = new Guid("3fa85f64-5717-4562-b3fc-2c963f66afa7"),
                    InstitutionName = "Institution 2",
                    BranchName = "Branch 2",
                    Website = "Website 2",
                    PhoneNumber = "Phone 2",
                    Summary = "Summary 2",
                    EstablishedOn = DateTime.Now,
                    Rate = 3.8,
                    AddressId = new Guid("3fa85f64-5717-4562-b3fc-2c963f66afa7"),
                    LogoId = "LogoId 2",
                    BannerId = "BannerId 2"
                }
            };

            var mockRepo = new Mock<IInstitutionProfileRepository>();

            mockRepo.Setup(r => r.GetAll()).ReturnsAsync(institutionProfiles);

            mockRepo.Setup(r => r.Add(It.IsAny<InstitutionProfile>())).ReturnsAsync((InstitutionProfile institutionProfile) =>
            {
                institutionProfile.Id = Guid.NewGuid();
                institutionProfiles.Add(institutionProfile);
                return institutionProfile;
            });

            mockRepo.Setup(r => r.Update(It.IsAny<InstitutionProfile>())).Callback((InstitutionProfile institutionProfile) =>
            {
                var existingProfile = institutionProfiles.FirstOrDefault(p => p.Id == institutionProfile.Id);
                if (existingProfile != null)
                {
                    existingProfile.InstitutionName = institutionProfile.InstitutionName;
                    existingProfile.BranchName = institutionProfile.BranchName;
                    existingProfile.Website = institutionProfile.Website;
                    existingProfile.PhoneNumber = institutionProfile.PhoneNumber;
                    existingProfile.Summary = institutionProfile.Summary;
                    existingProfile.EstablishedOn = institutionProfile.EstablishedOn;
                    existingProfile.Rate = institutionProfile.Rate;
                    existingProfile.AddressId = institutionProfile.AddressId;
                    existingProfile.LogoId = institutionProfile.LogoId;
                    existingProfile.BannerId = institutionProfile.BannerId;
                }
            });

            mockRepo.Setup(r => r.Delete(It.IsAny<InstitutionProfile>())).Callback((InstitutionProfile institutionProfile) =>
            {
                institutionProfiles.RemoveAll(p => p.Id == institutionProfile.Id);
            });

            mockRepo.Setup(r => r.Get(It.IsAny<Guid>())).ReturnsAsync((Guid id) =>
            {
                return institutionProfiles.FirstOrDefault(p => p.Id == id);
            });

            return mockRepo;
        }
    }
}
