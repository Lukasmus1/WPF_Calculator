namespace WPF_Calculator.Scripts;

public class Calculation
{
    private readonly TextParser _parser = new();
    private decimal? _result = null;
    public decimal? Result
    {
        get => _result;
        set => _result = value;
    }
    
    public void Calculate(string expression)
    {
        List<KeyValuePair<NumberTypes, string>> postfix = _parser.PostfixConversion(expression);
        

        
    }
}