﻿using Application.Contracts.Persistence;
using Application.UnitTest.Mocks;
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
            var mockInstitutionAvailabilityRepo =  MockInstitutionAvailabilityRepository.GetInstitutionAvailabilityRepository();
            var mockDoctorAvailabilityRepository = MockDoctorAvailabilityRepository.GetDoctorAvailabilityRepository();
            mockUow.Setup(r => r.InstitutionAvailabilityRepository).Returns(mockInstitutionAvailabilityRepo.Object);
            mockUow.Setup(r => r.DoctorAvailabilityRepository).Returns(mockDoctorAvailabilityRepo.Object);
            mockUow.Setup(r => r.Save()).ReturnsAsync(1);
            return mockUow;
        }


    }
}
