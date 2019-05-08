using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using CouponManager.Api.Data;
using CouponManager.Api.ViewModels;
using System.Net;
using System.Text.Encodings.Web;

namespace CouponManager.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _config;
        private readonly IMailSender _mailSender;

        public AuthController(
            UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager,
            RoleManager<IdentityRole> roleManager,
            IConfiguration config,
            IMailSender mailSender
        )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _config = config;
            _mailSender = mailSender;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> LoginAsync(LoginViewModel model)
        {
            var user = await _userManager.FindByNameAsync(model.UserName);

            if (user != null)
            {
                var isEmailConfirmed = await _userManager.IsEmailConfirmedAsync(user);
                if (isEmailConfirmed)
                {
                    var result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);
                    if (result.Succeeded)
                    {
                        var claims = new[]
                        {
                        new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                        new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim("UserId", user.Id),
                        new Claim("Role", user.IsAdmin ? "Admin" : "User")
                    };

                        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:SigningKey"]));
                        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                        var token = new JwtSecurityToken(issuer: _config["Jwt:Issuer"],
                            audience: _config["Jwt:Audience"],
                            claims: claims,
                            expires: DateTime.Now.AddMinutes(30),
                            signingCredentials: creds
                        );

                        return new JsonResult(new { token = new JwtSecurityTokenHandler().WriteToken(token) });
                    }
                    return new JsonResult(result);
                }
                var resendConfirmEmailUrl = Url.Action("ResendConfirmEmail", "Auth", Request.Scheme);
                return new JsonResult(new { Error = "Please confirm your email before logging in.", resendConfirmEmailUrl });
            }
            return BadRequest(new { Error = "User doesn't exist." });
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> RegisterAsync(RegisterViewModel model)
        {
            var user = new AppUser
            {
                Email = model.Email,
                UserName = model.UserName,
                IsAdmin = model.IsAdmin,
                Url = model.Url
            };

            var result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                if (user.IsAdmin) await _userManager.AddToRolesAsync(user, new string[] { "Admin", "User" });
                else await _userManager.AddToRoleAsync(user, "User");
                var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                var callbackUrl = Url.Action("ConfirmEmail", "Auth", new { userId = user.Id, code }, Request.Scheme);
                var mailModel = new SendEmailViewModel
                {
                    ToEmails = new string[] { user.Email },
                    Subject = "Confirm your email",
                    HtmlBody = $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.",
                    FromEmail = "noreply@couponmanager.com",
                    Name = user.UserName
                };
                await _mailSender.SendEmailAsync(mailModel);
            }

            return new JsonResult(result);
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> ResendConfirmEmail(LoginViewModel model)
        {
            var user = await _userManager.FindByNameAsync(model.UserName);
            var result = await _userManager.CheckPasswordAsync(user, model.Password);
            if (result)
            {
                var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                var callbackUrl = Url.Action("ConfirmEmail", "Auth", new { userId = user.Id, code }, Request.Scheme);
                var mailModel = new SendEmailViewModel
                {
                    ToEmails = new string[] { user.Email },
                    Subject = "Confirm your email",
                    HtmlBody = $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.",
                    FromEmail = "noreply@couponmanager.com",
                    Name = user.UserName
                };
                await _mailSender.SendEmailAsync(mailModel);
            }

            return new JsonResult(result);
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> ConfirmEmail(string userId, string code)
        {
            var user = await _userManager.FindByIdAsync(userId);
            var result = await _userManager.ConfirmEmailAsync(user, code);
            return new JsonResult(result);
        }

        [HttpGet("[action]/{userName}")]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> DeleteUser(string userName)
        {
            var user = await _userManager.FindByNameAsync(userName);
            var result = await _userManager.DeleteAsync(user);
            return new JsonResult(result);
        }

        [HttpPost]
        [Route("register/role")]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> RegisterRoleAsync(string roleName)
        {
            if (await _roleManager.RoleExistsAsync(roleName))
            {
                Response.StatusCode = (int)HttpStatusCode.NotModified;
                return new JsonResult(new { Result = "Role Already Exists!" });
            }
            var role = new IdentityRole(roleName);
            var result = await _roleManager.CreateAsync(role);
            return new JsonResult(result);
        }
    }
}