using Application.Contracts.Infrastructure;
using Application.Contracts.Persistence;
using Application.Features.Chat.CQRS.Handlers;
using Application.Features.Chat.CQRS.Queries;
using Application.Features.Chat.DTOs;
using Application.Features.Chat.Models;
using Application.Features.DoctorProfiles.DTOs;
using Application.Features.InstitutionProfiles.DTOs;
using AutoMapper;
using Domain;
using MediatR;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Application.Tests.Features.Chat.CQRS.Handlers
{
    public class ChatRequestQueryHandlerTests
    {
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly Mock<IChatRequestSender> _chatRequestSenderMock;
        private readonly ChatRequestQueryHandler _handler;

        public ChatRequestQueryHandlerTests()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _mapperMock = new Mock<IMapper>();
            _chatRequestSenderMock = new Mock<IChatRequestSender>();
            _handler = new ChatRequestQueryHandler(_unitOfWorkMock.Object, _mapperMock.Object, _chatRequestSenderMock.Object);
        }

        [Fact]
        public async Task Handle_WithValidRequest_ReturnsSuccessfulResponse()
        {
            // Arrange
            var request = new ChatRequestQuery { ChatRequestDto = new ChatRequestDto() };

            var apiResponse = new ApiResponseDto
            {
                Data = new Data { message = "Hello! How can I assist you today?", specializations = new List<string>() },
                Error = null
            };

            _chatRequestSenderMock.Setup(s => s.SendMessage(request.ChatRequestDto))
                .ReturnsAsync(apiResponse);

            // Act
            var result = await _handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Null(result.Error);

            var response = result.Value;
            Assert.NotNull(response);
            Assert.Equal(apiResponse.Data.message, response.reply);
            Assert.Null(response.Doctors);
            Assert.Null(response.Institutions);
        }

        [Fact]
        public async Task Handle_WithValidRequestAndSpecializations_ReturnsSuccessfulResponseWithDoctorsAndInstitutions()
        {
            // Arrange
            var request = new ChatRequestQuery { ChatRequestDto = new ChatRequestDto() };

            var apiResponse = new ApiResponseDto
            {
                Data = new Data
                {
                    message = "Some message",
                    specializations = new List<string> { "Specialization1", "Specialization2" }
                },
                Error = null
            };

            _chatRequestSenderMock.Setup(s => s.SendMessage(request.ChatRequestDto))
                .ReturnsAsync(apiResponse);

            var doctor1 = new DoctorProfile { /* doctor properties */ };
            var doctor2 = new DoctorProfile { /* doctor properties */ };
            var doctors = new List<DoctorProfile> { doctor1, doctor2 };

            var institution1 = new InstitutionProfile { /* institution properties */ };
            var institution2 = new InstitutionProfile { /* institution properties */ };
            var institutions = new List<InstitutionProfile> { institution1, institution2 };

            _unitOfWorkMock.Setup(u => u.DoctorProfileRepository.FilterDoctors(
                It.IsAny<Guid>(), It.IsAny<List<string>>(), It.IsAny<int>(), It.IsAny<string>()))
                .ReturnsAsync(doctors);

            _unitOfWorkMock.Setup(u => u.DoctorProfileRepository.FilterDoctors(
                It.IsAny<Guid>(), It.IsAny<List<string>>(), It.IsAny<int>(), It.IsAny<string>()))
                .ReturnsAsync(doctors);

            _mapperMock.Setup(m => m.Map<List<DoctorProfileDetailDto>>(doctors))
                .Returns(doctors.Select(d => new DoctorProfileDetailDto { /* map properties accordingly */ }).ToList());

            _mapperMock.Setup(m => m.Map<List<InstitutionProfileDetailDto>>(institutions))
                .Returns(institutions.Select(i => new InstitutionProfileDetailDto { /* map properties accordingly */ }).ToList());

            // Act
            var result = await _handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Null(result.Error);

            var response = result.Value;
            Assert.NotNull(response);
            Assert.Equal(apiResponse.Data.message, response.reply);
            Assert.NotNull(response.Doctors);
            Assert.Equal(doctors.Count, response.Doctors.Count);
        }

        [Fact]
        public async Task Handle_WithErrorResponse_ReturnsUnsuccessfulResponseWithError()
        {
            // Arrange
            var requestDto = new ChatRequestDto
            {
                message = "Emergency!",
                Address = "pokahggd",
                isNewChat = true
            };

            var apiError = new Error { message = "Error occurred while processing the request." };
            var apiResponse = new ApiResponseDto { Data = null, Error = apiError };

            _chatRequestSenderMock.Setup(mock => mock.SendMessage(requestDto)).ReturnsAsync(apiResponse);

            // Act
            var result = await _handler.Handle(new ChatRequestQuery { ChatRequestDto = requestDto }, CancellationToken.None);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal(apiError.message, result.Error);
            Assert.Null(result.Value);

            _chatRequestSenderMock.Verify(mock => mock.SendMessage(requestDto), Times.Once);
            _unitOfWorkMock.Verify(mock => mock.DoctorProfileRepository.FilterDoctors(Guid.Empty, It.IsAny<List<string>>(), -1, null), Times.Never);
             _mapperMock.Verify(mock => mock.Map<List<DoctorProfileDetailDto>>(It.IsAny<List<DoctorProfile>>()), Times.Never);
             _mapperMock.Verify(mock => mock.Map<List<InstitutionProfileDetailDto>>(It.IsAny<IEnumerable<InstitutionProfile>>()), Times.Never);
        }
    }
}
