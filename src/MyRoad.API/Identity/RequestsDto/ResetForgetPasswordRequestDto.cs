namespace MyRoad.API.Identity.RequestsDto;

public class ResetForgetPasswordRequestDto
{
    public long UserId { get; set; }
    public string Token { get; set; } = string.Empty;
    public string NewPassword { get; set; } = string.Empty;
    public string ConfirmNewPassword { get; set; } = string.Empty;
}