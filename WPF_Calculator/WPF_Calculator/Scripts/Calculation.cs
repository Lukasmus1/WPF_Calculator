using System.Globalization;

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

        Stack<string> stack = new();
        
        foreach (KeyValuePair<NumberTypes,string> item in postfix)
        {
            switch (item.Key)
            {
                case NumberTypes.Operand:
                    stack.Push(item.Value);
                    break;
                
                case NumberTypes.Operator:
                    //Exception for %
                    
                    decimal op1 = decimal.Parse(stack.Pop());
                    decimal op2 = decimal.Parse(stack.Pop());
                    
                    switch (item.Value)
                    {
                        case "+":
                            stack.Push((op2 + op1).ToString(CultureInfo.InvariantCulture));
                            break;
                        
                        case "-":
                            stack.Push((op2 - op1).ToString(CultureInfo.InvariantCulture));
                            break;
                        
                        case "*":
                            stack.Push((op2 * op1).ToString(CultureInfo.InvariantCulture));
                            break;
                        
                        case "/":
                            if (op1 == 0)
                            {
                                throw new DivideByZeroException();
                            }
                            stack.Push((op2 / op1).ToString(CultureInfo.InvariantCulture));
                            break;
                        
                        case "^":
                            stack.Push(Math.Pow((double)op2, (double)op1).ToString(CultureInfo.InvariantCulture));
                            break;
                        
                        case "√":
                            stack.Push(Math.Pow((double)op2, 1 / (double)op1).ToString(CultureInfo.InvariantCulture));
                            break;
                        
                        /*case "%":
                            stack.Push((op2 % op1).ToString());
                            break;*/
                        
                        default:
                            throw new NotImplementedException();
                    }
                    
                    break;
            }
        }
        
        if (stack.Count != 1 || !decimal.TryParse(stack.Peek(), out decimal res))
        {
            throw new Exception();
        }
        _result = res;
    }
}