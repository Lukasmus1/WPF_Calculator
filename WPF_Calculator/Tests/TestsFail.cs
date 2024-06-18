using WPF_Calculator.Scripts;

namespace Tests;

public class TestsFail
{
    [Theory]
    [InlineData("1/0")]
    [InlineData("/1")]
    [InlineData("2√(-1)")]
    public void TestFail(string input)
    {
        //Arrange
        Calculation calc = new();
        
        //Act & Assert
        Assert.ThrowsAny<Exception>(() => calc.Calculate(input));
    }
    
}