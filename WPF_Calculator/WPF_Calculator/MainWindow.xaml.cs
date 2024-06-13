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
    }

    private void OnClick(object sender, RoutedEventArgs e)
    {
        try
        {
            TextBoxBuilder.ParseClick(sender, MainTb);
        }
        catch (Exception)
        {
            MainTb.Text = "ERR";
        }
    }

    private void TextBoxInput(object sender, TextCompositionEventArgs e)
    {
        Regex regex = new Regex("[^0-9+-/*()%,\u221a^]+|[\\.]");
        e.Handled = regex.IsMatch(e.Text);
    }
}