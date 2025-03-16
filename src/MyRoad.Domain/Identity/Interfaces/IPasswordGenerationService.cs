namespace MyRoad.Domain.Identity.Interfaces;

public interface IPasswordGenerationService
{
    string GenerateRandomPassword(int length);
    
}