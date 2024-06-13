using System.Runtime.InteropServices.ComTypes;

namespace WPF_Calculator.Scripts;

public class TextParser
{

    private readonly Dictionary<string, int> _operatorPrecedence = new Dictionary<string, int>
    {
        { "(", 0 },
        { ")", 0 },
        { "+", 1 },
        { "-", 1 },
        { "*", 2 },
        { "/", 2 },
        { "^", 3 },
        { "√", 3 },
        { "%", 4 }
    };
    
    public List<KeyValuePair<NumberTypes, string>> PostfixConversion(string expression)
    {
        List<KeyValuePair<NumberTypes, string>> result = new();
        List<KeyValuePair<NumberTypes, string>> tokens = Tokenize(expression);
        Stack<KeyValuePair<NumberTypes, string>> stack = new();

        foreach (KeyValuePair<NumberTypes, string> token in tokens)
        {
            switch (token.Key)
            {
                // Operand
                case NumberTypes.Operand:
                    result.Add(token);
                    break;
                
                // Operator
                case NumberTypes.Operator:
                    int stackPre;
                    int tokenPre;
                    do
                    {
                        if (stack.Count == 0)
                        {
                            stack.Push(token);
                            break;
                        }
                        tokenPre = _operatorPrecedence[token.Value];
                        if (_operatorPrecedence.TryGetValue(stack.Peek().Value, out stackPre))
                        {
                            if (tokenPre > stackPre )
                            {
                                stack.Push(token);
                            }
                            else
                            {
                                KeyValuePair<NumberTypes, string> item = stack.Pop();
                                result.Add(item);
                            }
                        }
                    } while (stackPre >= tokenPre);
                    break;
                
                // (
                case NumberTypes.OpenParenthesis:
                    stack.Push(token);
                    break;
                
                // )
                case NumberTypes.CloseParenthesis:
                {
                    KeyValuePair<NumberTypes, string> top;
                    do
                    {
                        top = stack.Pop();
                        if (top.Key != NumberTypes.OpenParenthesis)
                        {
                            result.Add(top);
                        }
                    } while (top.Key != NumberTypes.OpenParenthesis);
                    break;
                }
            }
        }

        //Pop the stack 
        while (stack.Count > 0)
        {
            KeyValuePair<NumberTypes, string> item = stack.Pop();
            result.Add(item);
        }

        return result;
    }
    
    private List<KeyValuePair<NumberTypes, string>> Tokenize(string expression)
    {
        List<KeyValuePair<NumberTypes, string>> tokens = new();

        bool buildingNumber = false;
        string number = "";
        foreach (char c in expression)
        {
            if (char.IsDigit(c) || c == ',')
            {
                buildingNumber = true;
                number += c;
                continue;
            }
            else
            {
                if (buildingNumber)
                {
                    tokens.Add(new KeyValuePair<NumberTypes, string>(NumberTypes.Operand, number));
                    number = "";
                    buildingNumber = false;
                }
            }
            
            switch (c)
            {
                case '(':
                    tokens.Add(new KeyValuePair<NumberTypes, string>(NumberTypes.OpenParenthesis, c.ToString()));
                    break;
                case ')':
                    tokens.Add(new KeyValuePair<NumberTypes, string>(NumberTypes.CloseParenthesis, c.ToString()));
                    break;
                
                case '+':
                case '-':
                case '*':
                case '/':
                case '^':
                case '√':
                case '%':
                    tokens.Add(new KeyValuePair<NumberTypes, string>(NumberTypes.Operator, c.ToString()));
                    break;
                
                case ' ':
                    break;
                
                default:
                    Console.WriteLine("gg");
                    break;
            }
        }

        if (number != "")
        {
            tokens.Add(new KeyValuePair<NumberTypes, string>(NumberTypes.Operand, number));
        }
        
        return tokens;
    }
}