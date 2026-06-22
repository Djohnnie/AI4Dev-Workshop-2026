using System.Security.Cryptography;

namespace ShortUrlWorkshop.Services;

public sealed class ShortCodeGenerator : IShortCodeGenerator
{
    private const string Alphabet = "ABCDEFGHJKLMNPQRSTUVWXYZabcdefghijkmnopqrstuvwxyz23456789";

    public string Generate(int length = 7)
    {
        Span<byte> bytes = stackalloc byte[length];
        RandomNumberGenerator.Fill(bytes);

        var characters = new char[length];
        for (var index = 0; index < length; index++)
        {
            characters[index] = Alphabet[bytes[index] % Alphabet.Length];
        }

        return new string(characters);
    }
}
