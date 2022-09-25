﻿namespace BlogApp.MVCUI.Services.Results;

public class AuthResult
{
    public string Token { get; set; } = string.Empty;
    public string RefreshToken { get; set; } = string.Empty;
    public bool Success { get; set; }
    public List<string> Errors { get; set; }
}