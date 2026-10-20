using System;
using System.IO;
using System.Net.Http;
using System.Text.Json;
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
            var httpClient = new HttpClient
            {
                BaseAddress = LoadApiBaseAddress(),
            };
            var authenticationService = new AuthenticationService(httpClient);
            desktop.Exit += (_, _) => httpClient.Dispose();
            desktop.MainWindow = new MainWindow
            {
                DataContext = new MainViewModel(authenticationService),
            };
        }

        base.OnFrameworkInitializationCompleted();
    }

    private static Uri LoadApiBaseAddress()
    {
        var settingsPath = Path.Combine(AppContext.BaseDirectory, "appsettings.json");
        if (!File.Exists(settingsPath))
        {
            throw new FileNotFoundException("Client configuration file was not found.", settingsPath);
        }

        using var document = JsonDocument.Parse(File.ReadAllText(settingsPath));
        if (document.RootElement.ValueKind != JsonValueKind.Object
            || !document.RootElement.TryGetProperty("Api", out var apiSettings)
            || apiSettings.ValueKind != JsonValueKind.Object
            || !apiSettings.TryGetProperty("BaseUrl", out var baseUrlSetting)
            || baseUrlSetting.ValueKind != JsonValueKind.String
            || !Uri.TryCreate(baseUrlSetting.GetString(), UriKind.Absolute, out var baseAddress)
            || (baseAddress.Scheme != Uri.UriSchemeHttp && baseAddress.Scheme != Uri.UriSchemeHttps))
        {
            throw new InvalidDataException(
                "Client configuration must define Api:BaseUrl as an absolute HTTP or HTTPS URL.");
        }

        return new Uri(baseAddress.AbsoluteUri.TrimEnd('/') + "/");
    }
}