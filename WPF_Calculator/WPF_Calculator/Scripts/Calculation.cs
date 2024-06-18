using System.Globalization;

namespace WPF_Calculator.Scripts;

public class Calculation
{
    private readonly TextParser _parser = new();

    public decimal? Result { get; private set; } = null;

    public bool GotResult { get; set; } = false;

    public void Calculate(string expression)
    {
        //Throws an exception if the expression is invalid
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
                    decimal op1 = decimal.Parse(stack.Pop());
                    decimal op2 = decimal.Parse(stack.Pop());
                    
                    switch (item.Value)
                    {
                        case "+":
                            stack.Push((op2 + op1).ToString());
                            break;
                        
                        case "-":
                            stack.Push((op2 - op1).ToString());
                            break;
                        
                        case "*":
                            stack.Push((op2 * op1).ToString());
                            break;
                        
                        case "/":
                            if (op1 == 0)
                            {
                                throw new DivideByZeroException();
                            }
                            stack.Push((op2 / op1).ToString());
                            break;
                        
                        case "^":
                            stack.Push(Math.Pow((double)op2, (double)op1).ToString());
                            break;
                        
                        case "√":
                            if (op1 < 0)
                            {
                                throw new Exception();
                            }
                            stack.Push(Math.Sqrt((double)op1).ToString());
                            break;
                        
                        case "%":
                            stack.Push((op2 % op1).ToString());
                            break;
                        
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
        
        string tempRes = res.ToString();
        int index = tempRes.IndexOf(',');
        if (index != -1 && tempRes.Length - index > 15)
        {
            tempRes = tempRes[..(index + 16)];
        }
        
        Result = decimal.Parse(tempRes);
    }
}