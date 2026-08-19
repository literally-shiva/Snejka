using Client.ViewModels;

namespace Client.Services;

public class NavigationService
{
    private readonly LoginViewModel _loginViewModel;
    private readonly ChatViewModel _chatViewModel;
    private readonly SettingsViewModel _settingsViewModel;


    public NavigationService()
    {
        _loginViewModel = new LoginViewModel();
        _chatViewModel = new ChatViewModel();
        _settingsViewModel = new SettingsViewModel();
    }

    public LoginViewModel GetLoginViewModel() => _loginViewModel;

    public ChatViewModel GetChatViewModel() => _chatViewModel;

    public SettingsViewModel GetSettingsViewModel() => _settingsViewModel;
}
