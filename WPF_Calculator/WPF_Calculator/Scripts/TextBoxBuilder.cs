﻿using System.Globalization;
using System.Net.Http.Headers;
using System.Windows.Controls;
using System.Windows.Input;
using MyNamespace;

namespace WPF_Calculator.Scripts;

public class TextBoxBuilder
{
    private static readonly Calculation _calc = new();
    
    public static void ParseClick(object sender, object textBox)
    {
        if (textBox is TextBox tb && sender is Button button)
        {
            switch (button.Tag)
            {
                case ButtonTypes.Menu:
                    MenuButton();
                    break;
                
                case ButtonTypes.Writables:
                    AddToTextBox(tb, button.Content.ToString()!);
                    break;
                
                case ButtonTypes.OneOverX:
                    AddToTextBox(tb, "(1/");
                    break;
                
                case ButtonTypes.Clear:
                    ClearTextBox(tb);
                    break;
                
                case ButtonTypes.Backspace:
                    Backspace(tb);
                    break;
                
                case ButtonTypes.Pow:
                    AddToTextBox(tb, "^");
                    break;
                
                case ButtonTypes.Root:
                    AddToTextBox(tb, "√");
                    break;
                
                case ButtonTypes.Ans:
                    AddToTextBox(tb, _calc.Result != null ? _calc.Result.ToString() : string.Empty);
                    break;
                
                case ButtonTypes.Res:
                    Calculate(tb);
                    break;
                
                default:
                    throw new NotImplementedException();
                    
            }
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
            _calc.Calculate(tb.Text);
        }
        catch (DivideByZeroException )
        {
            tb.Text = Localization.GetString("ErrZeroDivision");
            return;
        }

        tb.Text = _calc.Result.ToString() ?? string.Empty;
    }
    
}