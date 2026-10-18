using Client.ViewModels;

namespace Client.Services;

public class NavigationService
{
    public NavigationService()
    {
        AuthenticationVm = new AuthenticationViewModel();
        ChatVm = new ChatViewModel();
        SettingsVm = new SettingsViewModel();
    }

    public AuthenticationViewModel AuthenticationVm { get; }

    public ChatViewModel ChatVm { get; }

    public SettingsViewModel SettingsVm { get; }
}
