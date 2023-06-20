using Application.Contracts.Infrastructure;
using Application.Contracts.Persistence;
using Application.Features.Chat.CQRS.Handlers;
using Application.Features.Chat.CQRS.Queries;
using Application.Features.Chat.DTOs;
using Application.Features.DoctorProfiles.DTOs;
using Application.Features.InstitutionProfiles.DTOs;
using Application.Responses;
using Application.UnitTest.Mocks;
using AutoMapper;
using Domain;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Application.UnitTest.Features.Chat.Handlers
{
    public class ChatRequestQueryHandlerTests
    {
        [Fact]
        public async Task Handle_NewChatRequest_ReturnsChatResponseWithDoctorsAndInstitutions()
        {
            // Arrange
            var requestDto = new ChatRequestDto
            {
                isNewChat = true,
                // Set other properties as needed
            };

            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var mockMapper = new Mock<IMapper>();
            var mockChatRequestSender = MockChatRequestSender.GetChatRequestSender();

            var handler = new ChatRequestQueryHandler(mockUnitOfWork.Object, mockMapper.Object, mockChatRequestSender.Object);

            var query = new ChatRequestQuery
            {
                ChatRequestDto = requestDto
            };

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.NotNull(result.Value);
            Assert.Equal("Welcome to the chat!", result.Value.reply);
            Assert.NotEmpty(result.Value.Doctors);
            Assert.NotEmpty(result.Value.Institutions);

            mockChatRequestSender.Verify(sender => sender.SendMessage(requestDto), Times.Once);
            mockUnitOfWork.Verify(uow => uow.DoctorProfileRepository.FilterDoctors(It.IsAny<Guid>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<string[]>()), Times.Once);
            mockUnitOfWork.Verify(uow => uow.InstitutionProfileRepository.GetMainInstitutionsForDoctors(It.IsAny<IEnumerable<DoctorProfile>>()), Times.Once);
        }

        [Fact]
        public async Task Handle_ExistingChatRequest_ReturnsChatResponseWithoutDoctorsAndInstitutions()
        {
            // Arrange
            var requestDto = new ChatRequestDto
            {
                isNewChat = false,
                // Set other properties as needed
            };

            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var mockMapper = new Mock<IMapper>();
            var mockChatRequestSender = MockChatRequestSender.GetChatRequestSender();

            var handler = new ChatRequestQueryHandler(mockUnitOfWork.Object, mockMapper.Object, mockChatRequestSender.Object);

            var query = new ChatRequestQuery
            {
                ChatRequestDto = requestDto
            };

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.NotNull(result.Value);
            Assert.Equal("Reply to the existing chat.", result.Value.reply);
            Assert.Empty(result.Value.Doctors);
            Assert.Empty(result.Value.Institutions);

            mockChatRequestSender.Verify(sender => sender.SendMessage(requestDto), Times.Once);
            mockUnitOfWork.Verify(uow => uow.DoctorProfileRepository.FilterDoctors(It.IsAny<Guid>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<string[]>()), Times.Never);
            mockUnitOfWork.Verify(uow => uow.InstitutionProfileRepository.GetMainInstitutionsForDoctors(It.IsAny<IEnumerable<DoctorProfile>>()), Times.Never);
        }
    }
}
