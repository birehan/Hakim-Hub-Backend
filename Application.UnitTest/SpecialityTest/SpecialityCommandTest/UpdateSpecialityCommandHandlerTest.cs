

using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Contracts.Persistence;
using Application.Features.Specialities.CQRS.Commands;
using Application.Features.Specialities.CQRS.Handlers;
using Application.Features.Specialities.DTOs;
using Application.Profiles;
using Application.Responses;
using Application.UnitTest.Mocks;
using AutoMapper;
using Domain;
using MediatR;
using Moq;
using Shouldly;
using Xunit;

namespace Application.UnitTest.SpecialityTest.SpecialityCommandTest;
    public class UpdateSpecialityCommandHandlerTest
    {
        private IMapper _mapper { get; set; }
        private readonly UpdateSpecialityCommandHandler _handler;
        private readonly CreateSpecialityDto createSpecialityDto;
        private CreateSpecialityCommandHandler _createHandler { get; set; }
        private Mock<IUnitOfWork> _mockUnitOfWork { get; set; }


        public UpdateSpecialityCommandHandlerTest()
        {
            _mapper = new MapperConfiguration(c =>
      {
          c.AddProfile<MappingProfile>();
      }).CreateMapper();
            _mockUnitOfWork = MockUnitOfWork.GetUnitOfWork();

            _handler = new UpdateSpecialityCommandHandler(_mockUnitOfWork.Object, _mapper);
            createSpecialityDto = new CreateSpecialityDto()
            {
                Id = Guid.NewGuid(),
                Name = "Psychiatrist",
                Description = "This is a short description about psychiatry."
            };

            _createHandler = new CreateSpecialityCommandHandler(_mockUnitOfWork.Object, _mapper);

        }

        [Fact]
        public async Task UpdateSpecialityValid()
        {

            var result = await _createHandler.Handle(new CreateSpecialityCommand() { SpecialityDto = createSpecialityDto }, CancellationToken.None);
            var educationToBeUpdated = result.Value.Id;


            var updateEducationDto = new UpdateSpecialityDto
            {
                Id = educationToBeUpdated,
               Name = "Updated",
               Description = "This is an updated description"

            };

            var updatedResult = await _handler.Handle(new UpdateSpecialityCommand() { SpecialityDto = updateEducationDto }, CancellationToken.None);

            updatedResult.Error.ShouldBeEquivalentTo("Speciality Updated Successfully.");
            Assert.IsType<Result<Unit?>>(updatedResult);
            result.IsSuccess.ShouldBeTrue();

        }

        [Fact]
        public async Task UpdateEducationInvalid_EducationNotFound()
        {

            var updateSpeciliatyDto = new UpdateSpecialityDto
            {
                Id = Guid.NewGuid(),
                Name = "Oncology",
                Description = "Sample Description"
            };

            var result = await _handler.Handle(new UpdateSpecialityCommand() { SpecialityDto = updateSpeciliatyDto }, CancellationToken.None);

            result.IsSuccess.ShouldBeFalse();
            result.Error.ShouldBe("Speciality Not Found.");
            result.Value.ShouldBeNull();
        }

        [Fact]
        public async Task UpdateEducationInvalid_ValidationFailed()
        {
            var result = await _createHandler.Handle(new CreateSpecialityCommand() { SpecialityDto = createSpecialityDto }, CancellationToken.None);
            var educationToBeUpdated = result.Value.Id;

            var updateEducationDto = new UpdateSpecialityDto
            {
                Id = educationToBeUpdated,
                Name = null,
                Description = "Sample description"
            };

            var resultAfterFailedUpdate = await _handler.Handle(new UpdateSpecialityCommand() { SpecialityDto = updateEducationDto }, CancellationToken.None);

            // Assert
            // result.IsSuccess.ShouldBeFalse();
            resultAfterFailedUpdate.Error.ShouldBe("Name is required.");
            resultAfterFailedUpdate.Value.ShouldBeNull();
        }
    }
