using BusinessLogic.DTOs.Accounts;

namespace BusinessLogic.Interfaces
{
    public interface IAccountsService
    {
        Task Register(RegisterModel model);
        Task Login(LoginModel model);
        Task Logout(LogoutModel model);
    }
}
