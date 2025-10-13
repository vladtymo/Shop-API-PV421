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
        private string? CurrentIp => HttpContext.Connection.RemoteIpAddress?.ToString();

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
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            var res = await accountsService.Login(model, CurrentIp);
            return Ok(res);
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout(LogoutModel model)
        {
            await accountsService.Logout(model);
            return Ok();
        }

        [HttpPost("refresh")]
        public async Task<IActionResult> Refresh(RefreshRequest model)
        {
            return Ok(await accountsService.Refresh(model, CurrentIp));
        }
    }
}
