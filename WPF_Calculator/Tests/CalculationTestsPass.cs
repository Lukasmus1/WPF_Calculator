using WPF_Calculator.Scripts;

namespace Tests;

public class CalculationTestsPass
{
    [Theory]
    [InlineData("1+1", 2)]
    [InlineData("1-1", 0)]
    [InlineData("1*1", 1)]
    [InlineData("1/1", 1)]
    [InlineData("2^3", 8)]
    [InlineData("10%2", 0)]
    [InlineData("10%3", 1)]
    [InlineData("2√9", 3)]
    [InlineData("2√9*5+5-8*2-4-21", -21)]
    public void BasicTests(string input, decimal output)
    {
        //Arrange
        Calculation calc = new();
        
        //Act
        calc.Calculate(input);
        
        //Assert
        Assert.Equal(output, calc.Result);
    }
    
    [Theory]
    [InlineData("1+1", 2)]
    public void AdvancedTests(string input, decimal output)
    {
        //Arrange
        Calculation calc = new();
        
        //Act
        calc.Calculate(input);
        
        //Assert
        Assert.Equal(output, calc.Result);
    }
}