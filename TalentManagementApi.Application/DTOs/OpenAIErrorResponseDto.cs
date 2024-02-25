using System.Text.Json.Serialization;

namespace TalentManagementApi.Application.DTOs
{
    public class OpenAIErrorResponseDto
    {
        [JsonPropertyName("error")]
        public OpenAIError Error { get; set; }
    }

    public class OpenAIError
    {
        [JsonPropertyName("message")]
        public string Message { get; set; }

        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("param")]
        public string Param { get; set; }

        [JsonPropertyName("code")]
        public string Code { get; set; }
    }
}