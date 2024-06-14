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
        SetHeightAndWidth(mainWindow);
        mainWindow.Show();
    }
    
    private static void SetHeightAndWidth(Window window)
    {
        window.Height = 0.5 * SystemParameters.PrimaryScreenHeight;
        window.Width = 0.2 * SystemParameters.PrimaryScreenWidth;
    }

}