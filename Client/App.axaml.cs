using System.Net.Http;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Client.Services;
using Client.ViewModels;
using Client.Views;

namespace Client;

public partial class App : Application
{
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            var httpClient = new HttpClient();
            var authenticationService = new AuthenticationService(httpClient);
            desktop.Exit += (_, _) => httpClient.Dispose();
            desktop.MainWindow = new MainWindow
            {
                DataContext = new MainViewModel(authenticationService),
            };
        }

        base.OnFrameworkInitializationCompleted();
    }
}