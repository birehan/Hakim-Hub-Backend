using Application.Contracts.Infrastructure;
using Application.Features.Chat.CQRS.Queries;
using Application.Features.Chat.DTOs;
using Application.Responses;
using Moq;
using System.Threading.Tasks;

namespace Application.UnitTest.Mocks
{
    public static class MockChatRequestSender
    {
        public static Mock<IChatRequestSender> GetChatRequestSender()
        {
            var mockSender = new Mock<IChatRequestSender>();

            mockSender.Setup(sender => sender.SendMessage(It.IsAny<ChatRequestDto>()))
                .ReturnsAsync((ChatRequestDto requestDto) =>
                {
                    var response = new Result<ApiResponse>();

                    // Simulate the behavior based on isNewChat flag
                    if (requestDto.isNewChat)
                    {
                        response.Value = new ChatApiResponse
                        {
                            message = "Welcome to the chat!",
                            specializations = new[] { "Specialization 1", "Specialization 2" }
                        };
                    }
                    else
                    {
                        response.Value = new ChatApiResponse
                        {
                            message = "Reply to the existing chat.",
                            specializations = new string[] { }
                        };
                    }

                    return response;
                });

            return mockSender;
        }
    }
}
