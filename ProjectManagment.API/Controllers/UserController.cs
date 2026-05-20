using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using ProjectManagment.API.Controllers;
using ProjectManagment.Domain.Entities.Identity;
using ProjectManagment.Domain.Interfaces.ICore;
using ProjectManagment.Domain.ViewModels.Common;
using ProjectManagment.Domain.ViewModels.User;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using static ProjectManagment.Domain.Enums.Enumeration;

namespace Workflow.API
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : CommonControllerBase
    {
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;
        private readonly IConfiguration config;
        private readonly IIdentityProvider identityProvider;

        public UserController(UserManager<User> _userManager, SignInManager<User> _signInManager, IConfiguration _config, IIdentityProvider _identityProvider)
        {
            userManager = _userManager;
            signInManager = _signInManager;
            config = _config;
            identityProvider = _identityProvider;
        }

        [HttpPost, Route("login")]
        [AllowAnonymous]
        public async Task<ActionResult> Login(LoginVM model)
        {
            if (!ModelState.IsValid) return GetActionResult(new ResponseModel
            {
                Result = null,
                Code = ResponseCodeEnum.InvalidCredentials,
                MessageFL = nameof(ResponseCodeEnum.InvalidCredentials),
                MessageSL = nameof(ResponseCodeEnum.InvalidCredentials)
            });

            var user = await userManager.FindByNameAsync(model.UserName);
            if (user == null) return GetActionResult(new ResponseModel
            {
                Result = null,
                Code = ResponseCodeEnum.InvalidCredentials,
                MessageFL = nameof(ResponseCodeEnum.InvalidCredentials),
                MessageSL = nameof(ResponseCodeEnum.InvalidCredentials)
            });

            var signInResult = signInManager.PasswordSignInAsync(user, model.Password, true, false).Result;
            if (!signInResult.Succeeded) return GetActionResult(new ResponseModel
            {
                Result = null,
                Code = ResponseCodeEnum.InvalidCredentials,
                MessageFL = nameof(ResponseCodeEnum.InvalidCredentials),
                MessageSL = nameof(ResponseCodeEnum.InvalidCredentials)
            });

            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:SecretKey"]));
            var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

            var options = new IdentityOptions();
            var claims = new List<Claim>
            {
                new Claim(options.ClaimsIdentity.UserNameClaimType, user.UserName),
                new Claim(options.ClaimsIdentity.UserIdClaimType, user.Id),
            };

            var tokeOptions = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.UtcNow.AddHours(Convert.ToInt32(config["Jwt:ExpiresInHours"])),
                signingCredentials: signinCredentials
            );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);
            return GetActionResult(new ResponseModel
            {
                Result = tokenString,
                Code = ResponseCodeEnum.Success
            });
        }

        [HttpGet("logout")]
        [Authorize]
        public async Task<ActionResult> LogOut()
        {
            var user = identityProvider.GetUser();
            if (user != null)
            {
                await signInManager.SignOutAsync();
            }
            return GetActionResult(new ResponseModel
            {
                Code = ResponseCodeEnum.Success
            });
        }

        [HttpPost("add-user")]
        [AllowAnonymous]
        public async Task<ActionResult> AddUser(UserAddVM model)
        {
            if (!ModelState.IsValid) return GetActionResult(new ResponseModel
            {
                Code = ResponseCodeEnum.BadRequest,
                MessageFL = nameof(ResponseCodeEnum.BadRequest),
                MessageSL = nameof(ResponseCodeEnum.BadRequest)
            });

            var user = new User
            {
                Id = Guid.NewGuid().ToString(),
                UserName = model.UserName,
                Email = model.Email,
                EmailConfirmed = true,
                PhoneNumber = model.PhoneNumber
            };
            var result = await userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded) return GetActionResult(new ResponseModel
            {
                Code = ResponseCodeEnum.InternalServerError,
                MessageFL = nameof(ResponseCodeEnum.InternalServerError),
                MessageSL = nameof(ResponseCodeEnum.InternalServerError)
            });

            return GetActionResult(new ResponseModel
            {
                Code = ResponseCodeEnum.Success,
                MessageFL = null,
                MessageSL = null
            });
        }
    }
}