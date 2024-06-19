using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WPF_Calculator.Scripts;
using Localization = MyNamespace.Localization;

namespace WPF_Calculator;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        DataObject.AddPastingHandler(MainTb, OnPaste);
        MainTb.Focus();
    }

    private void OnPaste(object sender, DataObjectPastingEventArgs e)
    {
        if (e.DataObject.GetDataPresent(typeof(string)))
        {
            string text = (string)e.DataObject.GetData(typeof(string))!;

            if (!IsValidInput(text))
            {
                e.CancelCommand();
            }
            
            if (TextBoxBuilder.Calc.GotResult)
            {
                TextBoxBuilder.Calc.GotResult = false;
            }
        }
        else
        {
            e.CancelCommand();
        }
    }
    private static bool IsValidInput(string text)
    {
        Regex regex = new Regex("[^0-9+-/*()%,\u221a^]+|[\\.]");
        return !regex.IsMatch(text);
    }

    private void HelpOnClick(object sender, RoutedEventArgs e)
    {
        MessageBox.Show(Localization.GetString("Help"), Localization.GetString("HelpLabel"), MessageBoxButton.OK, MessageBoxImage.Information);
    }
    
    private void CalculatorOnClick(object sender, RoutedEventArgs e)
    {
        TextBoxBuilder.ParseClick(sender, MainTb);
        MainTb.Focus();
        MainTb.CaretIndex = MainTb.Text.Length;
    }

    private void TextBoxInput(object sender, TextCompositionEventArgs e)
    {
        if (TextBoxBuilder.Calc.GotResult)
        {
            TextBoxBuilder.Calc.GotResult = false;
        }
        
        Regex regex = new("[^0-9+-/*()%,\u221a^]+|[\\.]");
        e.Handled = regex.IsMatch(e.Text);
    }
}