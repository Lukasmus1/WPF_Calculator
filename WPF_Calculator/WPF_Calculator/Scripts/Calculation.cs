namespace WPF_Calculator.Scripts;

public class Calculation
{
    private double _result;
    public double Result
    {
        get => _result;
        set => _result = value;
    }
    
    public void Calculate(string expression)
    {
        TextParser parser = new();
        List<KeyValuePair<NumberTypes, string>> postfix = parser.PostfixConversion(expression);
        Console.WriteLine(postfix);
    }
}