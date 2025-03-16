namespace MyRoad.Domain.Identity.Responses;

public class RegisterResponsesDto
{
    public string? Message { get; set; }
    public bool IsCreate { get; set; }
    
    public string password { get; set; } // just for testing i will remove it when i finish test 
}