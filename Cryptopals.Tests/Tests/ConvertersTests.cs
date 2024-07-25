namespace Cryptopals.Tests.Tests;
using Xunit;

public class ConvertersTests
{
    
    [Fact] 
    public void HexToBase64()
    {
        var result = Converters.HexToBase64("6261746D616E");
        Assert.Equal("YmF0bWFu", result);
    }
    
    [Fact] 
    public void HexToBytes()
    {
        var result = Converters.HexToBytes("6261746D616E");
        var expected = "batman"u8.ToArray(); // converting text string straight to byte array
        Assert.Equal(expected, result);
    }
}