namespace CoreProjectAPI.Models.DTO.Auth;

public record LoginResponseDto(string Email, string Token, List<string> Roles);