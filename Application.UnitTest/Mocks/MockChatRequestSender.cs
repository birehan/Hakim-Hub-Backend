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
                    // Simulate the API response based on the request
                    var response = new ApiResponseDto();
                    if (request.message == "hi")
                    {
                        response.Value = new ChatApiResponse
                        {
                            message = "Hello! How can I assist you today?",
                            specialization = ""
                        };
                        response.Error = null;
                    }
                    else if (request.message == "I am sick")
                    {
                        response.Data = new Data
                        {
                            message = "Based on your symptoms...",
                            specialization = "General Practitioner" 
                        };
                        response.Error = null;
                    }
                    else if (request.message == "Emergency!")
                    {
                        response.Data = null;
                        response.Error = new Error { message = "Error occurred while processing the request." };
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
