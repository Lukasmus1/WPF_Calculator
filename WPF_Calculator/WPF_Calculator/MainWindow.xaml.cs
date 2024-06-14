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
    }

    private void OnPaste(object sender, DataObjectPastingEventArgs e)
    {
        if (e.DataObject.GetDataPresent(typeof(String)))
        {
            String text = (String)e.DataObject.GetData(typeof(String));
            if (!IsValidInput(text))
            {
                e.CancelCommand();
            }
        }
        else
        {
            e.CancelCommand();
        }
    }
    private bool IsValidInput(string text)
    {
        Regex regex = new Regex("[^0-9+-/*()%,\u221a^]+|[\\.]");
        return !regex.IsMatch(text);
    }

    private void OnClick(object sender, RoutedEventArgs e)
    {
        TextBoxBuilder.ParseClick(sender, MainTb);
    }

    private void TextBoxInput(object sender, TextCompositionEventArgs e)
    {
        Regex regex = new Regex("[^0-9+-/*()%,\u221a^]+|[\\.]");
        e.Handled = regex.IsMatch(e.Text);
    }
}