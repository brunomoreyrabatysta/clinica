using Clinica.Core.Requests.Account;
using Clinica.Core.Responses;

namespace Clinica.Core.Handlers;

public interface IAccountHandler
{
    Task<Response<string>> LoginAsync(LoginRequest request);
    Task<Response<string>> RegisterAsync(RegisterRequest request);
    Task LogoutAsync();
}