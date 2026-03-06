using System;
using System.Text.Json.Serialization;

namespace API.DTOs;

public class UserDto
{
    [JsonPropertyName("id")]
    public required string ID { get; set; }
    [JsonPropertyName("email")]
    public required string Email { get; set; }
    [JsonPropertyName("displayName")]
    public required string DisplayName { get; set; }
    [JsonPropertyName("imageUrl")]
    public string? ImageUrl { get; set; }
    [JsonPropertyName("token")]
    public required string Token { get; set; }


}
