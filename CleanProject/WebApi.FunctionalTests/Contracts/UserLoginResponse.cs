﻿namespace WebApi.FunctionalTests.Contracts;

internal sealed class UserLoginResponse
{
    public string TokenType { get; set; }
    public string AccessToken { get; set; }
    public int ExpiresIn { get; set; }
    public string RefreshToken { get; set; }
}