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
    
    [Fact]
    public void BytesToHex()
    {
        var result = Converters.BytesToHex("batman"u8.ToArray());
        Assert.Equal("6261746D616E", result);
    }
    
    [Fact]
    public void TransposeArraysOfBytes()
    {
        IEnumerable<byte[]> blocks = new List<byte[]>
        {
            "ABC"u8.ToArray(),
            "DEF"u8.ToArray(),
            "GHI"u8.ToArray()
        };
        IEnumerable<byte[]> expected = new List<byte[]>
        {
            "ADG"u8.ToArray(),
            "BEH"u8.ToArray(),
            "CFI"u8.ToArray()
        };
        var result = Converters.TransposeArraysOfBytes(blocks);
        Assert.Equal(expected, result);
    }
}