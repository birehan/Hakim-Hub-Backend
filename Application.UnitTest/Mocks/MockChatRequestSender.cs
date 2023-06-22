using Application.Contracts.Infrastructure;
using Application.Features.Chat.DTOs;
using Application.Features.Chat.Models;
using Moq;
using System.Threading.Tasks;

namespace Application.Tests.Mocks.Infrastructure
{
    public static class ChatRequestSenderMockFactory
    {
        public static Mock<IChatRequestSender> Create()
        {
            var mock = new Mock<IChatRequestSender>();

            // Mock the SendMessage method
            mock.Setup(sender => sender.SendMessage(It.IsAny<ChatRequestDto>()))
                .ReturnsAsync((ChatRequestDto request) =>
                {
                    // Simulate the API response based on the request
                    var response = new ApiResponseDto();
                    if (request.message == "hi")
                    {
                        response.Data = new Data
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
                        response.Data = null;
                        response.Error = null;
                    }
                    return response;
                });

            return mock;
        }
    }
}
