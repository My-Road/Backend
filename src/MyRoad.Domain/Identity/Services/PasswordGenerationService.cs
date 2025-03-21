using System.Text;
using MyRoad.Domain.Identity.Interfaces;

namespace MyRoad.Domain.Identity.Services;

public class PasswordGenerationService : IPasswordGenerationService
{
    private const string Lowercase = "abcdefghijklmnopqrstuvwxyz";
    private const string Uppercase = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
    private const string Digits = "0123456789";
    private const string SpecialChars = "!@#$%^&*()_+-=[]{}|;':\",.<>?";
    private const string CharsCombined = $"{Lowercase}{Uppercase}{Digits}{SpecialChars}";
    private static readonly Random Random = new();

    public string GenerateRandomPassword(int length)
    {
        if (length < 8)
            throw new ArgumentException("Password length must be at least 8 to include all character types.");

        var passwordBuilder = new StringBuilder(length);

        passwordBuilder.Append(Lowercase[Random.Next(Lowercase.Length)]);
        passwordBuilder.Append(Uppercase[Random.Next(Uppercase.Length)]);
        passwordBuilder.Append(Digits[Random.Next(Digits.Length)]);
        passwordBuilder.Append(SpecialChars[Random.Next(SpecialChars.Length)]);

        for (var i = 4; i < length; i++)
        {
            passwordBuilder.Append(CharsCombined[Random.Next(CharsCombined.Length)]);
        }

        return new string(passwordBuilder.ToString().OrderBy(_ => Random.Next()).ToArray());
    }
}