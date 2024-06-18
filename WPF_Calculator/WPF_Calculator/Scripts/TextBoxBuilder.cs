using System.Globalization;
using System.Net.Http.Headers;
using System.Windows.Controls;
using System.Windows.Input;
using MyNamespace;

namespace WPF_Calculator.Scripts;

public class TextBoxBuilder
{
    public static readonly Calculation Calc = new();
    
    public static void ParseClick(object sender, object textBox)
    {
        if (textBox is TextBox tb && sender is Button button)
        {
            string content;
            switch (button.Tag)
            {
                case ButtonTypes.Menu:
                    MenuButton();
                    return;
                
                case ButtonTypes.Writables:
                    content = button.Content.ToString()!;
                    break;
                
                case ButtonTypes.OneOverX:
                    content = "(1/";
                    break;
                
                case ButtonTypes.Clear:
                    ClearTextBox(tb);
                    return;
                
                case ButtonTypes.Backspace:
                    Backspace(tb);
                    return;
                
                case ButtonTypes.Pow:
                    content = "^";
                    break;
                
                case ButtonTypes.Root:
                    content = "2√(";
                    break;
                
                case ButtonTypes.Ans:
                    content = Calc.Result != null ? Calc.Result.ToString()! : string.Empty;
                    break;
                
                case ButtonTypes.Res:
                    Calculate(tb);
                    return;
                
                default:
                    throw new NotImplementedException();
                    
            }
            
            if (Calc.GotResult)
            {
                ClearTextBox(tb);
                Calc.GotResult = false;
            }
            
            AddToTextBox(tb, content);
        }
    }
    
    private static void AddToTextBox(TextBox tb, string? content)
    {
        tb.Text += content;
    }

    private static void ClearTextBox(TextBox tb)
    {
        tb.Text = string.Empty;
    }
    
    private static void Backspace(TextBox tb)
    {
        if (tb.Text.Length > 0)
        {
            tb.Text = tb.Text.Remove(tb.Text.Length - 1);
        }
    }
    
    private static void MenuButton()
    {
        
    }
    
    private static void Calculate(TextBox tb)
    {
        try
        {
            Calc.Calculate(tb.Text);
            Calc.GotResult = true;
        }
        catch (DivideByZeroException )
        {
            tb.Text = Localization.GetString("ErrZeroDivision");
            return;
        }
        catch (Exception)
        {
            tb.Text = Localization.GetString("ErrInvalidExpression");
            return;
        }

        tb.Text = Calc.Result.ToString() ?? string.Empty;
    }
    
}