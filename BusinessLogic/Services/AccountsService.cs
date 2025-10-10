using AutoMapper;
using Azure.Core;
using BusinessLogic.DTOs.Accounts;
using BusinessLogic.Interfaces;
using DataAccess.Data;
using DataAccess.Data.Entities;
using Microsoft.AspNetCore.Identity;
using System.Net;

namespace BusinessLogic.Services
{
    public class AccountsService : IAccountsService
    {
        private readonly IJwtService jwtService;
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;
        private readonly IMapper mapper;

        // Identity services: UserManager, SignInManager, RoleManager
        public AccountsService(
            IJwtService jwtService,
            UserManager<User> userManager, 
            SignInManager<User> signInManager, 
            IMapper mapper)
        {
            this.jwtService = jwtService;
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.mapper = mapper;
        }

        public async Task<LoginResponse> Login(LoginModel model)
        {
            var user = await userManager.FindByEmailAsync(model.Email);

            if (user == null || !await userManager.CheckPasswordAsync(user, model.Password))
                throw new HttpException("Invalid email or password.", HttpStatusCode.BadRequest);

            //await signInManager.SignInAsync(user, true);
            return new()
            {
                AccessToken = jwtService.GenerateToken(jwtService.GetClaims(user))
            };
        }

        public async Task Logout(LogoutModel model)
        {
            await signInManager.SignOutAsync();
        }

        public async Task Register(RegisterModel model)
        {
            var user = mapper.Map<User>(model);

            var result = await userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded) 
                throw new HttpException(result.Errors?.FirstOrDefault()?.Description ?? "Error", HttpStatusCode.BadRequest);
        }
    }
}
