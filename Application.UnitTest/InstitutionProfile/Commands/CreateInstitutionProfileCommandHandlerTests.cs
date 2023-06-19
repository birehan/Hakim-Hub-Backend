using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Contracts.Persistence;
using Application.Features.InstitutionProfiles.CQRS.Commands;
using Application.Features.InstitutionProfiles.CQRS.Handlers;
using Application.Features.InstitutionProfiles.DTOs;
using Application.Features.InstitutionProfiles.DTOs.Validators;
using Application.UnitTest.Mocks;
using Application.Interfaces;
using Application.Responses;
using Microsoft.AspNetCore.Http;
using Application.Photos;
using AutoMapper;
using Domain;
using Moq;
using Shouldly;
using Xunit;

namespace Application.UnitTest.InstitutionProfiles.Commands
{
    public class CreateInstitutionProfileCommandHandlerTests
    {
        private readonly Mock<IUnitOfWork> unitOfWorkMock;
        private readonly Mock<IMapper> mapperMock;
        private readonly Mock<IPhotoAccessor> photoAccessorMock;
        private readonly CreateInstitutionProfileCommandHandler handler;

        public CreateInstitutionProfileCommandHandlerTests()
        {

            unitOfWorkMock = MockUnitOfWork.GetUnitOfWork();
            mapperMock = new Mock<IMapper>();
            photoAccessorMock = new Mock<IPhotoAccessor>();
            handler = new CreateInstitutionProfileCommandHandler(unitOfWorkMock.Object, mapperMock.Object, photoAccessorMock.Object);
        }
        

        [Fact]
        public async Task Handle_ValidCommand_ReturnsSuccessResultWithInstitutionProfileId()
        {
            Console.WriteLine("✨ Executing Handle_ValidCommand_ReturnsSuccessResultWithInstitutionProfileId...");

            // Arrange
            var institutionProfileDto = new CreateInstitutionProfileDto
            {
                InstitutionName = "Institution 1",
                BranchName = "Branch 1",
                Website = "www.Website.com",
                PhoneNumber = "Phone 1",
                Summary = "Summary 1",
                EstablishedOn = DateTime.Now.AddDays(-10),
                Rate = 4.5
            };
            var command = new CreateInstitutionProfileCommand
            {
                CreateInstitutionProfileDto = institutionProfileDto
            };

            var photoUploadResult = new PhotoUploadResult
            {
                PublicId = "1",
                Url = "test_image.jpg"
            };

            photoAccessorMock.Setup(x => x.AddPhoto(It.IsAny<IFormFile>())).ReturnsAsync(photoUploadResult);

            var institutionProfile = new InstitutionProfile { Id = Guid.NewGuid() };
            mapperMock.Setup(x => x.Map<InstitutionProfile>(institutionProfileDto)).Returns(institutionProfile);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            result.IsSuccess.ShouldBeTrue();
            result.Value.ShouldBe(institutionProfile.Id);
            unitOfWorkMock.Verify(x => x.Save(), Times.Exactly(1));

            Console.WriteLine("✅ Handle_ValidCommand_ReturnsSuccessResultWithInstitutionProfileId executed successfully.");
        }

        [Fact]
        public async Task Handle_InvalidCommand_ReturnsFailureResult()
        {
            Console.WriteLine("✨ Executing Handle_InvalidCommand_ReturnsFailureResult...");

            // Arrange
            var institutionProfileDto = new CreateInstitutionProfileDto
            {
                BranchName = "Branch 1",
                Website = "www.Website.com",
                PhoneNumber = "Phone 1",
                Summary = "Summary 1",
                EstablishedOn = DateTime.Now.AddDays(-10),
                Rate = 4.5
            };
            var command = new CreateInstitutionProfileCommand
            {
                CreateInstitutionProfileDto = institutionProfileDto
            };

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            result.IsSuccess.ShouldBeFalse();
            result.Error.ShouldNotBeEmpty();
            result.Error.ShouldBe("Institution Name is required.");
            unitOfWorkMock.Verify(x => x.Save(), Times.Never());

            Console.WriteLine("❌ Handle_InvalidCommand_ReturnsFailureResult executed successfully.");
        }
    }

}
