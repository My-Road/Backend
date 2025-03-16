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


        var password = new char[length];
        password[0] = Lowercase[Random.Next(Lowercase.Length)];
        password[1] = Uppercase[Random.Next(Uppercase.Length)];
        password[2] = Digits[Random.Next(Digits.Length)];
        password[3] = SpecialChars[Random.Next(SpecialChars.Length)];

        for (var i = 4; i < length; i++)
        {
            password[i] = CharsCombined[Random.Next(CharsCombined.Length)];
        }

        return new string(password.OrderBy(_ => Random.Next()).ToArray());
    }
}