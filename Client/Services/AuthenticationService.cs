using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;

namespace Client.Services;

public sealed class AuthenticationService
{
    private const string RegisterEndpoint = "api/Auth/register";
    private readonly HttpClient _httpClient;

    public AuthenticationService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<RegistrationResult> RegisterAsync(string username, string password)
    {
        using var response = await _httpClient.PostAsJsonAsync(
            RegisterEndpoint,
            new RegisterRequest(username, password));

        if (response.IsSuccessStatusCode)
        {
            return new RegistrationResult(true, null);
        }

        var responseBody = await response.Content.ReadAsStringAsync();
        var errorMessage = TryGetErrorMessage(responseBody);
        errorMessage ??= response.StatusCode == HttpStatusCode.Conflict
            ? "Это имя пользователя уже занято."
            : $"Не удалось зарегистрироваться. Код ответа: {(int)response.StatusCode}.";

        return new RegistrationResult(false, errorMessage);
    }

    private static string? TryGetErrorMessage(string responseBody)
    {
        if (string.IsNullOrWhiteSpace(responseBody))
        {
            return null;
        }

        try
        {
            using var document = JsonDocument.Parse(responseBody);
            return document.RootElement.ValueKind == JsonValueKind.Object
                && document.RootElement.TryGetProperty("message", out var message)
                ? message.GetString()
                : null;
        }
        catch (JsonException)
        {
            return null;
        }
    }

    private sealed record RegisterRequest(string Username, string PasswordHash);
}

public sealed record RegistrationResult(bool IsSuccess, string? ErrorMessage);
