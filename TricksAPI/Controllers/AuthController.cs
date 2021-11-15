using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using TricksAPI.Data.Dtos.Auth;
using TricksAPI.Auth;
using TricksAPI.Auth.Model;
using AutoMapper;

namespace TricksAPI.Controllers
{
    [ApiController]
    [AllowAnonymous]
    [Route("api")]

    public class AuthController : ControllerBase
    {
        private readonly UserManager<RestUser> _userManager;
        private readonly IMapper _mapper;
        private readonly ITokenManager _tokenManager;

        public AuthController(UserManager<RestUser> userManager, IMapper mapper, ITokenManager tokenManager)
        {
            _userManager = userManager;
            _mapper = mapper;
            _tokenManager = tokenManager;
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register(RegisterUserDto resgisterUserDto)
        {
            var user = await _userManager.FindByEmailAsync(resgisterUserDto.Email);
            if (user != null)
                return BadRequest("Email is already registered.");

            var newUser = new RestUser
            {
                Email = resgisterUserDto.Email,
                UserName = resgisterUserDto.UserName
            };

            var createUserResult = await _userManager.CreateAsync(newUser, resgisterUserDto.Password);
            if (!createUserResult.Succeeded)
                return BadRequest("Could not create a user");

            await _userManager.AddToRoleAsync(newUser, RestUserRoles.SimpleUser);
            return CreatedAtAction(nameof(Register), _mapper.Map<UserDto>(newUser));
        }

        [HttpPost]
        [Route("login")]
        public async Task<ActionResult> Login (LoginUserDto loginUserDto)
        {
            var user = await _userManager.FindByEmailAsync(loginUserDto.Email);
            if (user == null)
                return BadRequest("User email or password is invalid.");

            var isPasswordValid = await _userManager.CheckPasswordAsync(user, loginUserDto.Password);
            if (!isPasswordValid)
                return BadRequest("User email or password is invalid.");

            var accessToken = await _tokenManager.CreateAccessTokenAsync(user);
            return Ok(new SuccessfulLoginResponseDto(accessToken));
        }

        [HttpPost]
        [Route("update")]
        public async Task<IActionResult> UpdateUser(UpdateUserDto updateUserDto)
        {
            var user = await _userManager.FindByEmailAsync(updateUserDto.Email);
            if (user == null)
                return BadRequest("Email not found.");

            await _userManager.AddToRoleAsync(user, RestUserRoles.PremiumUser);
            return Ok("User updated.");
        }
    }
}
