using System;
using System.ComponentModel.DataAnnotations;

namespace API.DTOs;

public class RegisterDto
{
    [Required]
public string DisplayName { get; set; }="";
[Required]
public required string Email { get; set; }="";
[Required]
[MinLength(4)]
public required string Password { get; set; }="";

}
