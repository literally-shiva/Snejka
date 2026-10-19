using Client.ViewModels;

namespace Client.Services;

public class NavigationService
{
    public NavigationService(AuthenticationService authenticationService)
    {
        AuthenticationVm = new AuthenticationViewModel(authenticationService);
        ChatVm = new ChatViewModel();
        SettingsVm = new SettingsViewModel();
    }

    public AuthenticationViewModel AuthenticationVm { get; }

    public ChatViewModel ChatVm { get; }

    public SettingsViewModel SettingsVm { get; }
}
