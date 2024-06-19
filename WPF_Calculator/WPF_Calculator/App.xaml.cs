using System.Configuration;
using System.Data;
using System.Windows;

namespace WPF_Calculator;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    void App_Startup(object sender, StartupEventArgs e)
    {
        MainWindow mainWindow = new MainWindow();
        SetHeightAndWidth(mainWindow, 0.5 * SystemParameters.PrimaryScreenHeight, 0.2 * SystemParameters.PrimaryScreenWidth);
        SetMinHeightAndWidth(mainWindow, 0.3 * SystemParameters.PrimaryScreenHeight, 0.2 * SystemParameters.PrimaryScreenWidth);
        SetHeightAndWidth(mainWindow.HelpButton, 0.03 * SystemParameters.PrimaryScreenHeight, 0.03 * SystemParameters.PrimaryScreenWidth);
        mainWindow.Show();
    }
    
    private static void SetHeightAndWidth(FrameworkElement obj, double height, double width)
    {
        obj.Height = height;
        obj.Width = width;
    }

    private static void SetMinHeightAndWidth(Window window, double height, double width)
    {
        window.MinHeight = height;
        window.MinWidth = width;
    }
    
}