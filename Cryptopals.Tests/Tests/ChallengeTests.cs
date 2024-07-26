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
        Assert.Equal("0B3637272A2B2E63622C2E69692A23693A2A3C6324202D623D63343C2A26226324272765272A282B2F20430A652E2C652A3124333A653E2B2027630C692B20283165286326302E27282F", result);
    }
}