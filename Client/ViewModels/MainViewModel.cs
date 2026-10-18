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
