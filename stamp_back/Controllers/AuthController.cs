using AutoMapper;
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

        [HttpGet("user")]
        public IActionResult User()
        {
            try
            {
                var jwt = Request.Cookies["Jwt"];

                var token = _jwtService.Verify(jwt);

                var userIdClaim = token.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);

                if (userIdClaim == null)
                    return Unauthorized("No user ID in token.");

                var userId = Guid.Parse(userIdClaim.Value);

                var user = _userRepository.GetUserById(userId);

                return Ok(user);
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
