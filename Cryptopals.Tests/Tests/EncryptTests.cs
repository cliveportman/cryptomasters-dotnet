namespace Cryptopals.Tests.Tests;

using Xunit;

public class EncryptTests
{
    
    [Fact]
    public void RepeatingKey()
    {
        var result = Encrypt.RepeatingKey("I'm vengeance!", "BAT").ToLower();
        Assert.Equal("0b66396237312c2631232f372760", result);
    }
}