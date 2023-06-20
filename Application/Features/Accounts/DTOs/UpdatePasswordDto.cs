namespace Application.Features.Accounts.DTOs
{
public class UpdatePasswordDto
{
    public string Email {get; set;}
    public string OldPassword { get; set; }
    public string NewPassword { get; set; }
}
}