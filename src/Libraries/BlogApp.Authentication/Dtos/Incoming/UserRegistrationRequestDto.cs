namespace BlogApp.Authentication.Dtos.Incoming;
public class UserRegistrationRequestDto
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string ConfirmedPassword { get; set; }
    public string IpAddress { get; set; }
}
