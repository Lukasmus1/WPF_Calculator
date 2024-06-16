using WPF_Calculator.Scripts;

namespace Tests;

public class PostfixTesting
{
    [Theory]
    [InlineData("1+1", "11+")]
    [InlineData("1-1", "11-")]
    [InlineData("1*1", "11*")]
    [InlineData("1/1", "11/")]
    [InlineData("1^1", "11^")]
    [InlineData("1√1", "11√")]
    [InlineData("1%1", "11%")]
    [InlineData("2+2+3", "22+3+")]
    [InlineData("2-2+3", "22-3+")]
    [InlineData("2*2/3", "22*3/")]
    [InlineData("2*2√3%5", "223√*5%")]
    [InlineData("2^2√3", "22^3√")]
    [InlineData("2^9^7^8^9^7", "29^7^8^9^7^")]
    [InlineData("10*45+8*7^2", "1045*872^*+")]
    [InlineData("10*4^5+8*7/10*48/7^5^2/7/8*100-4+5", "1045^*87*10/48*75^2^/7/8/100*+4-5+")]
    public void BasicTests(string input, string expected)
    {
        //Arrange
        TextParser parser = new();
        
        //Act
        List<KeyValuePair<NumberTypes, string>> postfix = parser.PostfixConversion(input);
        string result = string.Join("", postfix.Select(x => x.Value));
        
        //Assert 
        Assert.Equal(expected, result);
    }
    
    [Theory]
    [InlineData("0,1+0,02", "0,10,02+")]
    [InlineData("0,2      5     *0    ,25", "0,250,25*")]
    [InlineData("1+(1-1)", "111-+")]
    [InlineData("1+(-1)", "101-+")]
    [InlineData("1+8*(1-1/7)", "18117/-*+")]
    [InlineData("(1+8)*(1-(1/7))", "18+117/-*")]
    [InlineData("(1+8)*(1-(1/7^(8^9)))", "18+11789^^/-*")]
    [InlineData("-15+5", "015-5+")]
    [InlineData("-((5^10)*5^(8*9-(10+5)))", "0510^589*105+-^*-")]
    public void AdvancedTests(string input, string expected)
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