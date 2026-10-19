using System.Net.Http;
using System.Threading.Tasks;
using Client.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Client.ViewModels;

public partial class RegisterViewModel : ViewModelBase
{
    private readonly AuthenticationService _authenticationService;

    public RegisterViewModel(AuthenticationService authenticationService)
    {
        _authenticationService = authenticationService;
    }

    [ObservableProperty]
    public partial string Username { get; set; } = string.Empty;

    [ObservableProperty]
    public partial string Password { get; set; } = string.Empty;

    [ObservableProperty]
    public partial string ConfirmPassword { get; set; } = string.Empty;

    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(RegisterUserCommand))]
    public partial bool IsRegistering { get; set; }

    [ObservableProperty]
    public partial string StatusMessage { get; set; } = string.Empty;

    [RelayCommand(CanExecute = nameof(CanRegisterUser))]
    private async Task RegisterUserAsync()
    {
        if (string.IsNullOrWhiteSpace(Username))
        {
            StatusMessage = "Введите имя пользователя.";
            return;
        }

        if (string.IsNullOrWhiteSpace(Password))
        {
            StatusMessage = "Введите пароль.";
            return;
        }

        if (Password != ConfirmPassword)
        {
            StatusMessage = "Пароли не совпадают.";
            return;
        }

        IsRegistering = true;
        StatusMessage = string.Empty;

        try
        {
            var result = await _authenticationService.RegisterAsync(Username, Password);

            if (result.IsSuccess)
            {
                StatusMessage = "Регистрация прошла успешно.";
                Password = string.Empty;
                ConfirmPassword = string.Empty;
                return;
            }

            StatusMessage = result.ErrorMessage ?? "Не удалось зарегистрироваться.";
        }
        catch (HttpRequestException)
        {
            StatusMessage = "Не удалось подключиться к серверу. Убедитесь, что сервер запущен.";
        }
        finally
        {
            IsRegistering = false;
        }
    }

    private bool CanRegisterUser() => !IsRegistering;
}
