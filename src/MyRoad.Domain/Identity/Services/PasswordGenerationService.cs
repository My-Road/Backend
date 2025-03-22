using System.Text;
using MyRoad.Domain.Identity.Interfaces;
using ErrorOr;
namespace MyRoad.Domain.Identity.Services;

public class PasswordGenerationService : IPasswordGenerationService
{
    private const string Lowercase = "abcdefghijklmnopqrstuvwxyz";
    private const string Uppercase = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
    private const string Digits = "0123456789";
    private const string SpecialChars = "!@#$%^&*()_+-=[]{}|;':\",.<>?";
    private const string CharsCombined = $"{Lowercase}{Uppercase}{Digits}{SpecialChars}";
    private static readonly Random Random = new();

    public ErrorOr<string> GenerateRandomPassword(int length)
    {
        if (length < 8)
            return Error.Validation("Password.ShortLength", "Password length must be at least 8 characters.");

        var passwordBuilder = new StringBuilder(length);

        passwordBuilder.Append(Lowercase[Random.Next(Lowercase.Length)]);
        passwordBuilder.Append(Uppercase[Random.Next(Uppercase.Length)]);
        passwordBuilder.Append(Digits[Random.Next(Digits.Length)]);
        passwordBuilder.Append(SpecialChars[Random.Next(SpecialChars.Length)]);

        for (var i = 4; i < length; i++)
        {
            passwordBuilder.Append(CharsCombined[Random.Next(CharsCombined.Length)]);
        }

        return passwordBuilder.ToString();
    }
}