using System.Net.Http;
using System.Text;
using System.Text.Json;
using Application.Contracts.Infrastructure;
using Application.Features.Chat.DTOs;
using Application.Features.Chat.Models;

namespace Infrastructure.Chat;

public class ChatRequestSender: IChatRequestSender
{
    private readonly HttpClient _httpClient;
    private readonly JsonSerializerOptions _jsonOptions;

    public ChatRequestSender(HttpClient httpClient)
    {
        _httpClient = httpClient;
        _jsonOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };
    }

    public async Task<ApiResponseDto> SendMessage(string message, string ipAddress, bool isNewChat)
    {
        var requestBody = new ApiRequestDto
        {
            message = message
        };

        var requestBodyJson = JsonSerializer.Serialize(requestBody, _jsonOptions);
        var content = new StringContent(requestBodyJson, Encoding.UTF8, "application/json");

        _httpClient.DefaultRequestHeaders.Clear();
        _httpClient.DefaultRequestHeaders.Add("Address", ipAddress);

        string baseUri = "https://hakimhub-chatbot.onrender.com/api/chat/";
        baseUri += isNewChat.ToString();
        var response = await _httpClient.PostAsync(baseUri, content);

        if (!response.IsSuccessStatusCode)
        {
            // Handle API error
            return new ApiResponseDto
            {
                Error = new Error
                {
                    message = "Failed to communicate with the chatbot API."
                }
            };
        }

        var responseContent = await response.Content.ReadAsStringAsync();
        var apiResponse = JsonSerializer.Deserialize<ApiResponseDto>(responseContent, _jsonOptions);

        return apiResponse;
    }
}
