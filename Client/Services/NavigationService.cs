using Client.ViewModels;

namespace Client.Services;

public class NavigationService
{
    private readonly AuthenticationViewModel _authenticationVm;
    private readonly ChatViewModel _chatVm;
    private readonly SettingsViewModel _settingsVm;


    public NavigationService()
    {
        _authenticationVm = new AuthenticationViewModel();
        _chatVm = new ChatViewModel();
        _settingsVm = new SettingsViewModel();
    }

    public AuthenticationViewModel GetAuthenticationVm() => _authenticationVm;

    public ChatViewModel GetChatVm() => _chatVm;

    public SettingsViewModel GetSettingsVm() => _settingsVm;
}
