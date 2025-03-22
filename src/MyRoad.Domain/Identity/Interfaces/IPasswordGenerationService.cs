using ErrorOr;
namespace MyRoad.Domain.Identity.Interfaces;
public interface IPasswordGenerationService
{
    ErrorOr<string> GenerateRandomPassword(int length);
    
}