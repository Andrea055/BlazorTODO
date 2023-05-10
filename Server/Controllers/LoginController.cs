using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using todo_blazor_auth.Client.FormModel;
using todo_blazor_auth.Shared.Models;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    public class LoginController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;

        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger<LoginController> _logger;

        public LoginController(UserManager<ApplicationUser> userManager, ILogger<LoginController> logger, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
        }
        [HttpPost]
        [AllowAnonymous]
        async public Task<IActionResult> Login([FromBody] AccountLogin account)
        {
            try
            {
                if (account == null)
                {
                    return StatusCode(statusCode: 401);
                }
                else
                {
                    var user = await _userManager.FindByEmailAsync(account.Email) ?? throw new Exception("User not found");
                    var is_same_password = _userManager.PasswordHasher.VerifyHashedPassword(user, user.PasswordHash, account.Password);
                    if(is_same_password == PasswordVerificationResult.Success){
                        await _signInManager.SignInAsync(user, true);
                        return Ok();
                    }else
                    {
                        return StatusCode(401);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("ERROR DURING LOGIN", ex);
                return StatusCode(500);
            }
        }
    }
}