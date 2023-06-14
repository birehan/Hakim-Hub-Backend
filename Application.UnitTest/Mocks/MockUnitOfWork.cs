using Application.Contracts.Persistence;
using Application.UnitTest.Mocks;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UnitTest.Mocks
{
    public static class MockUnitOfWork
    {
        public static int changes = 0;
        public static Mock<IUnitOfWork> GetUnitOfWork()
        {
            var mockUow = new Mock<IUnitOfWork>();
            
            var mockEducationRepo = MockEducationRepository.GetEducationRepository();
            mockUow.Setup(r => r.EducationRepository).Returns(mockEducationRepo.Object);

            var mockSpecialityRepo = MockSpecialityRepository.GetSpecialityRepository();
            mockUow.Setup(r => r.SpecialityRepository).Returns(mockSpecialityRepo.Object);

            mockUow.Setup(r => r.Save()).ReturnsAsync(1);
            return mockUow;
        }
    }
}
