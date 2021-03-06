using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using NETCore.MailKit.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dictionarium.Models;
using Dictionarium.Extensions.Verification;

namespace Dictionarium.Controllers
{
    [ApiController]
    [Route("api/")]
    public class UserRegistrationController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IEmailService _emailService;
        private readonly ILogger _logger;

        public UserRegistrationController(
            UserManager<IdentityUser> userManager,
            IEmailService emailService,
            IConfiguration config,
            ILoggerFactory loggerFactory)
        {
            _userManager = userManager;
            _emailService = emailService;
            _logger = loggerFactory.CreateLogger<UserRegistrationController>();
        }

        [HttpPost("Register")]
        public async Task<ActionResult> Post(UserModel identityUser)
        {
            if (Verification.VerifyEmail(identityUser.Email) == false)
            {
                _logger.LogWarning($"{DateTime.Now} WARNING. User: {identityUser.Email} tried to registry with the same" +
                    $" email throught WebUi using API");
                return StatusCode(409, "Incorrect email address");
            }

            var findEmail = _userManager.FindByEmailAsync(identityUser.Email).Result;
            if (findEmail != null)
            {
                _logger.LogWarning($"{DateTime.Now} WARNING. User: {identityUser.Email} tried to registry with the same" +
                    $" on WebAPI");
                return StatusCode(409, "User already exist. Wanna recover your account?");
            }

            var user = new IdentityUser
            {
                UserName = identityUser.FirstName,
                Email = identityUser.Email,
            };

            var result = await _userManager.CreateAsync(user);
            if (result.Succeeded)
            {
                await _userManager.AddPasswordAsync(user, identityUser.Password);
                var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                var link = Url.Action(nameof(VerifyEmail), "Register", new { userId = user.Id, code }, Request.Scheme, Request.Host.ToString());
                await _emailService.SendAsync(identityUser.Email, "Account Confirmation", Verification.VerificationMessage(identityUser.FirstName, link), true);
                _logger.LogInformation($"{DateTime.Now} INFO. User: {identityUser.Email} registered account");
                return StatusCode(200, $"Check {user.Email} to verificate your account");
            }
            return StatusCode(400, "Some Thing Gone Wrong");
        }


        [ApiExplorerSettings(IgnoreApi = true)]
        public async Task<ActionResult> VerifyEmail(string userId, string code)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return StatusCode(400, "User not found");
            var result = await _userManager.ConfirmEmailAsync(user, code);
            if (result.Succeeded)
            {
                _logger.LogInformation($"{DateTime.Now} INFO. User: {userId} succeeded done registration");
                return Redirect("https://HaveToChangeIt");
            }
            _logger.LogError($"{DateTime.Now} ERROR. User: {userId} got error while verified account");
            return Redirect("https://HaveToChangeIt");
        }
    }
}
