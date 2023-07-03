using System.Text.Json.Serialization;

namespace BlogApp.Authentication.Dtos.Incoming;
public record UserRegistrationRequestDto
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string ConfirmedPassword { get; set; } = string.Empty;

    [JsonIgnore(Condition = JsonIgnoreCondition.Always)]
    public string IpAddress { get; set; } = string.Empty;
}
