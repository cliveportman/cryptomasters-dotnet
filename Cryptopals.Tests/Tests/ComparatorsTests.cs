namespace Cryptopals.Tests.Tests;

using Xunit;

public class ComparatorsTests
{
    [Fact]
    public void Xor()
    {
        var result = Comparators.Xor("batman"u8.ToArray(), "barman"u8.ToArray());
        Assert.Equal(new byte[] { 0, 0, 6, 0, 0, 0 }, result);
    }

    [Fact]
    public void Xor_UnequalLengthStrings_ThrowsException()
    {
        var exception =
            Assert.Throws<ArgumentException>(() => Comparators.Xor("batman"u8.ToArray(), "bat"u8.ToArray()));
        Assert.Equal("Byte arrays must be of equal length", exception.Message);
    }

    [Fact]
    public void ScoreText()
    {
        var result = Comparators.ScoreText("What time is love?");
        Assert.Equal(348, result);
    }

    [Fact]
    public void HammingDistance()
    {
        var text1Bytes = System.Text.Encoding.ASCII.GetBytes("this is a test");
        var text2Bytes = System.Text.Encoding.ASCII.GetBytes("wokka wokka!!!");
        var result = Comparators.HammingDistance(text1Bytes, text2Bytes);
        Assert.Equal(37, result);
    }
}