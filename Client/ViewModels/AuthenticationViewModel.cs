using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Client.ViewModels;

public partial class AuthenticationViewModel : ViewModelBase
{
    [ObservableProperty]
    public partial ViewModelBase CurrentViewModel { get; set; }

    [RelayCommand]
    private void Login() => CurrentViewModel = new LoginViewModel();

    [RelayCommand]
    public void Register() => CurrentViewModel = new RegisterViewModel();
}
