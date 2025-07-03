using ExampleMvvmPattern.Implementation;
using ExampleMvvmPattern.ViewModels;
using ExampleMvvmPattern.Views;
using System.Configuration;
using System.Data;
using System.Windows;

namespace ExampleMvvmPattern
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var dialogService = new DialogService();
            var mainViewModel = new MainWindowViewModel(dialogService);
            var mainWindow = new MainWindow
            {
                DataContext = mainViewModel
            };
            mainWindow.Show();
        }
    }

}
