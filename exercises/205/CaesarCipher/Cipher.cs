namespace CaesarCipher;

// Use the GitHub Copilot /tests command to generate tests for this class
public static class Cipher
{
    public static string Encrypt(string text, int shift)
    {
        shift = ((shift % 26) + 26) % 26;
        return new string(text.Select(c => ShiftChar(c, shift)).ToArray());
    }

    public static string Decrypt(string text, int shift)
    {
        return Encrypt(text, -shift);
    }

    private static char ShiftChar(char c, int shift)
    {
        if (!char.IsLetter(c))
            return c;

        char origin = char.IsUpper(c) ? 'A' : 'a';
        return (char)(origin + (c - origin + shift) % 26);
    }
}