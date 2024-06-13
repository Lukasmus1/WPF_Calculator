using WPF_Calculator.Scripts;

namespace Tests;

public class PostfixTesting
{
    [Theory]
    [InlineData("1+1", "11+")]
    [InlineData("1-1", "11-")]
    [InlineData("1*1", "11*")]
    [InlineData("1/1", "11/")]
    public void Validate(string input, string expected)
    {
        //Arrange
        TextParser parser = new();
        
        //Act
        List<KeyValuePair<NumberTypes, string>> postfix = parser.PostfixConversion(input);
        string result = string.Join("", postfix.Select(x => x.Value));
        
        //Assert 
        Assert.Equal(expected, result);
    }
}