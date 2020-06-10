using System;
using System.Threading.Tasks;
using DAL.App.EF.Helpers;
using Domain;
using Domain.Identity;
using Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using PublicApi.DTO.v1;

namespace WebApp.ApiControllers.Identity
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class AccountController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly UserManager<AppUser> _userManager;
        private readonly ILogger<AccountController> _logger;
        private readonly SignInManager<AppUser> _signInManager;

        public AccountController(IConfiguration configuration, UserManager<AppUser> userManager,
            ILogger<AccountController> logger, SignInManager<AppUser> signInManager)
        {
            _configuration = configuration;
            _userManager = userManager;
            _logger = logger;
            _signInManager = signInManager;
        }
        
        /// <summary>
        /// Login user
        /// </summary>
        /// <param name="model">Login DTO</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<string>> Login([FromBody] LoginDTO model)
        {
            var appUser = await _userManager.FindByEmailAsync(model.Email);
            if (appUser == null)
            {
                _logger.LogInformation($"Web-Api login. User {model.Email} not found!");
                return StatusCode(403);
            }

            var result = await _signInManager.CheckPasswordSignInAsync(appUser, model.Password, false);
            if (result.Succeeded)
            {
                var claimsPrincipal = await _signInManager.CreateUserPrincipalAsync(appUser); //get the User analog
                var jwt = IdentityExtensions.GenerateJWT(claimsPrincipal.Claims,
                    _configuration["JWT:SigningKey"],
                    _configuration["JWT:Issuer"],
                    _configuration.GetValue<int>("JWT:ExpirationInDays")
                );
                _logger.LogInformation($"Token generated for user {model.Email}");
                return Ok(new {token = jwt, status = "Logged in"});
            }

            _logger.LogInformation($"Web-Api login. User {model.Email} attempted to log-in with bad password!");
            return StatusCode(403);
        }

        /// <summary>
        /// Registers user
        /// </summary>
        /// <param name="model">RegisterDTO</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<string>> Register([FromBody] RegisterDTO model)
        {
            var userExists = await _userManager.FindByEmailAsync(model.Email);

            if (userExists != null)
            {
                _logger.LogInformation($"Web-Api register. User {model.Email} already exists!");
                return StatusCode(403);
            }

            var user = new AppUser()
            {
                UserName = model.Email,
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName,
            };

            await _userManager.CreateAsync(user, model.Password);
            await _userManager.AddToRoleAsync(user, RoleNames.RoleGuest);
            await _userManager.UpdateAsync(user);
            
            return Ok(200);
        }
        
        /// <summary>
        /// Update user
        /// </summary>
        /// <param name="model">RegisterDTO</param>
        /// <returns></returns>
        [HttpPut]
        public async Task<ActionResult<string>> Update([FromBody] RegisterDTO model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                return BadRequest();
            }

            user.Email = model.Email;
            user.UserName = model.Email;
            user.FirstName = model.FirstName;
            user.LastName = model.LastName;

            await _userManager.UpdateAsync(user);

            return Ok(200);
        }

        [HttpGet("{userName}")]
        public async Task<ActionResult<RegisterDTO>> GetUser(string userName)
        {
            var currentUser = await _userManager.FindByEmailAsync(userName);
            var user = new RegisterDTO()
            {
                Email = currentUser.Email,
                FirstName = currentUser.FirstName,
                LastName = currentUser.LastName
            };

            return Ok(user);
        }

    }
}