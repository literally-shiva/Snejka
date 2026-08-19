using Client.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Client.ViewModels;

public partial class MainViewModel : ViewModelBase
{
    private NavigationService _navigationService;

    public MainViewModel()
    {
        _navigationService = new NavigationService();

        CurrentViewModel = _navigationService.GetLoginViewModel();
    }

    [ObservableProperty]
    public partial ViewModelBase CurrentViewModel { get; set; }

    [RelayCommand]
    private void Login() => _navigationService.GetLoginViewModel();

    [RelayCommand]
    private void Chat() => _navigationService.GetChatViewModel();

    [RelayCommand]
    private void Settings() => _navigationService.GetSettingsViewModel();
}
