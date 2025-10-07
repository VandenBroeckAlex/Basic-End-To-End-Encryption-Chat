using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.CookiePolicy;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using stamp_back.Dto;
using stamp_back.Helper;
using stamp_back.Interfaces;
using stamp_back.Models;
using System.Security.Claims;

namespace stamp_back.Controllers
{
    public class AuthController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly JwtService _jwtService;
        public AuthController(IUserRepository userRepository, IMapper mapper, JwtService jwtService) 
        {
            _mapper = mapper;
            _userRepository = userRepository;
            _jwtService = jwtService;
        }

        [HttpPost("login")] 
        public async Task<TokenResponseDto> Login(LoginDto.UserRegisterDto dto)
        {
            User user = _userRepository.GetUserByEmail(dto.Email);

            if (user == null)
            {
                return null;
            }

            if (!BCrypt.Net.BCrypt.Verify(dto.Password, user.Password))
            {
                return null;
            }

            TokenResponseDto response = await CreateTokenResponse(user);

            return response;
        }

        private async Task<TokenResponseDto> CreateTokenResponse(User user)
        {
            return new TokenResponseDto
            {
                AccessToken = _jwtService.GenerateToken(user),
                RefreshToken = await _jwtService.GenerateAndSaveRefreshTokenAsync(user)
            };
        }
        [Authorize]
        [HttpGet("user")]
        public IActionResult GetUser()
        {
            try
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                var email = User.FindFirst(ClaimTypes.Email)?.Value;

                return Ok(new
                {
                    UserId = userId,
                    Email = email
                });
            }
            catch (Exception ex)
            {
                return Unauthorized(ex.Message);
            }
           
        }

        [HttpPost("refresh-token")]
        public async Task<ActionResult<TokenResponseDto>> RefreshToken(RefreshTokenRequestDto request)
        {
            var result = await _jwtService.RefreshTokenAsync(request);
            if(result is null || result.AccessToken is null ||  result.RefreshToken is null)
            {
                return Unauthorized("Invalid Refresh Token");
            }
            return Ok(result);
        }

        [HttpPost("logout")]
        public IActionResult Logout()
        {
            Response.Cookies.Delete("Jwt");

            return Ok(new
            {
                message = "success"
            });
        }
    }
}
