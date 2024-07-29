namespace Cryptopals.Tests.Tests;

using Xunit;

public class DecryptTests
{
    [Fact]
    public void SingleCharacterXor()
    {
        var result = Decrypt.SingleCharacterXor("1b37373331363f78151b7f2b783431333d78397828372d363c78373e783a393b3736");
        Assert.Equal('X', result.Character);
        Assert.Equal(586, result.Score);
        Assert.Equal("Cooking MC's like a pound of bacon", result.DecryptedText);
    }
    
    [Fact]
    public void RepeatingKeyXor()
    {
        var result = Decrypt.RepeatingKeyXor("I'm vengeance!", "BAT").ToLower();
        Assert.Equal("0b66396237312c2631232f372760", result);
    }
}