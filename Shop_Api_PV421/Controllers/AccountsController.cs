using BusinessLogic.DTOs.Accounts;
using BusinessLogic.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Shop_Api_PV421.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IAccountsService accountsService;

        public AccountsController(IAccountsService accountsService)
        {
            this.accountsService = accountsService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            await accountsService.Register(model);
            return Ok();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginModel model)
        {
            await accountsService.Login(model);
            return Ok();
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout(LogoutModel model)
        {
            await accountsService.Logout(model);
            return Ok();
        }
    }
}
