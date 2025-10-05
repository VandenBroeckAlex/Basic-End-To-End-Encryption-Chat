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
        public IActionResult Login(LoginDto.UserRegisterDto dto)
        {
          User user =  _userRepository.GetUserByEmail(dto.Email);

            if (user == null)
            {
                return BadRequest(new { message = "Invalid Credentials" });
            }

            if(!BCrypt.Net.BCrypt.Verify(dto.Password, user.Password))
            {
                return BadRequest(new { message = "Invalid Credentials" });
            }

            var jwt = _jwtService.GenerateToken(user);

            return Ok(new { token = jwt });
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
