using System;

namespace API.DTOs;

public class UserDto
{
public required string ID { get; set; }
 public required string Email { get; set; }
public required string DisplayName { get; set; }
public string? ImagUrl { get; set; }
public required string Token { get; set; } 


}
