namespace Cryptopals.Tests.Tests;
using Xunit;

public class ChallengeTests
{
    [Fact] 
    public void Challenge1()
    {
        var result = Challenges.Challenge1.Run();
        Assert.Equal("SSdtIGtpbGxpbmcgeW91ciBicmFpbiBsaWtlIGEgcG9pc29ub3VzIG11c2hyb29t", result);
    }
    
    [Fact] 
    public void Challenge2()
    {
        var result = Challenges.Challenge2.Run();
        Assert.Equal("746865206b696420646f6e277420706c6179", result);
    }
    
    [Fact] 
    public void Challenge3()
    {
        var result = Challenges.Challenge3.Run();
        Assert.Equal("X: 586 Cooking MC's like a pound of bacon", result);
    }
    
    [Fact] 
    public void Challenge4()
    {
        var result = Challenges.Challenge4.Run();
        Assert.Equal("5: 551 Now that the party is jumping", result);
    }
    
    [Fact]
    public void Challenge5()
    {
        var result = Challenges.Challenge5.Run();
        Assert.Equal("0b3637272a2b2e63622c2e69692a23693a2a3c6324202d623d63343c2a26226324272765272a282b2f20430a652e2c652a3124333a653e2b2027630c692b20283165286326302e27282f", result);
    }
}