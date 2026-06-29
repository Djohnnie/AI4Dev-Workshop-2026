using Xunit;
using CaesarCipher;

namespace CaesarCipher.Tests;

public class CipherTests
{
    // --- Encrypt ---

    [Fact]
    public void Encrypt_ShiftZero_ReturnsSameText()
    {
        Assert.Equal("hello", Cipher.Encrypt("hello", 0));
    }

    [Fact]
    public void Encrypt_ShiftTwentySix_ReturnsSameText()
    {
        Assert.Equal("hello", Cipher.Encrypt("hello", 26));
    }

    [Fact]
    public void Encrypt_EmptyString_ReturnsEmpty()
    {
        Assert.Equal(string.Empty, Cipher.Encrypt(string.Empty, 5));
    }

    [Fact]
    public void Encrypt_NonLetterCharacters_ArePreserved()
    {
        Assert.Equal("Mjqqt, Btwqi!", Cipher.Encrypt("Hello, World!", 5));
    }

    [Fact]
    public void Encrypt_NegativeShift_ShiftsBackward()
    {
        Assert.Equal("abc", Cipher.Encrypt("bcd", -1));
    }

    [Theory]
    [InlineData("abc", 1, "bcd")]
    [InlineData("xyz", 3, "abc")]
    [InlineData("ABC", 3, "DEF")]
    [InlineData("XYZ", 3, "ABC")]
    [InlineData("abc", 13, "nop")]
    [InlineData("Hello", 5, "Mjqqt")]
    [InlineData("hello", 26, "hello")]
    public void Encrypt_KnownInputs_ReturnsExpected(string text, int shift, string expected)
    {
        Assert.Equal(expected, Cipher.Encrypt(text, shift));
    }

    // --- Decrypt ---

    [Fact]
    public void Decrypt_ShiftZero_ReturnsSameText()
    {
        Assert.Equal("hello", Cipher.Decrypt("hello", 0));
    }

    [Fact]
    public void Decrypt_EmptyString_ReturnsEmpty()
    {
        Assert.Equal(string.Empty, Cipher.Decrypt(string.Empty, 5));
    }

    [Fact]
    public void Decrypt_NonLetterCharacters_ArePreserved()
    {
        Assert.Equal("Hello, World!", Cipher.Decrypt("Mjqqt, Btwqi!", 5));
    }

    [Fact]
    public void Decrypt_RoundTrip_ReturnsOriginalText()
    {
        const string original = "The quick brown fox!";
        Assert.Equal(original, Cipher.Decrypt(Cipher.Encrypt(original, 17), 17));
    }
}
