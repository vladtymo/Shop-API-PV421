using BusinessLogic.DTOs.Accounts;
using System.Net;

namespace BusinessLogic.Interfaces
{
    public interface IAccountsService
    {
        Task Register(RegisterModel model);
        Task<LoginResponse> Login(LoginModel model, string? ipAddress);
        Task Logout(LogoutModel model);
        Task<LoginResponse> Refresh(RefreshRequest model, string? ipAddress);
    }
}
