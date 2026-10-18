using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Client.ViewModels;

public partial class AuthenticationViewModel : ViewModelBase
{
    public AuthenticationViewModel()
    {
        LoginVm = new LoginViewModel();
        RegisterVm = new RegisterViewModel();
    }

    public LoginViewModel LoginVm { get; }

    public RegisterViewModel RegisterVm { get; }

    [ObservableProperty]
    public partial ViewModelBase CurrentViewModel { get; set; }

    [RelayCommand]
    private void Login() => CurrentViewModel = LoginVm;

    [RelayCommand]
    public void Register() => CurrentViewModel = RegisterVm;
}
