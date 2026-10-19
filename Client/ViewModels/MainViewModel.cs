using Client.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Client.ViewModels;

public partial class MainViewModel : ViewModelBase
{
    private readonly NavigationService _navigationService;

    public MainViewModel(AuthenticationService authenticationService)
    {
        _navigationService = new NavigationService(authenticationService);

        CurrentViewModel = _navigationService.AuthenticationVm;
    }

    [ObservableProperty]
    public partial ViewModelBase CurrentViewModel { get; set; }

    [RelayCommand]
    private void Authentication() => CurrentViewModel = _navigationService.AuthenticationVm;

    [RelayCommand]
    private void Chat() => CurrentViewModel = _navigationService.ChatVm;

    [RelayCommand]
    private void Settings() => CurrentViewModel = _navigationService.SettingsVm;
}
