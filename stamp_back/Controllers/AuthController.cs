using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using stamp_back.Interfaces;
using stamp_back.Dto;


using stamp_back.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using stamp_back.Helper;
using Microsoft.AspNetCore.CookiePolicy;

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

            var jwt = _jwtService.generate(user.Id);

            Response.Cookies.Append("Jwt", jwt, new CookieOptions
            {
                HttpOnly = true
            })
            ;

            return Ok(new
            {
                message = "success"
            });
        }

        [HttpGet("user")]
        public IActionResult User()
        {
            try
            {
                var jwt = Request.Cookies["Jwt"];

                var token = _jwtService.Verify(jwt);

                var userID = Guid.Parse(token.Issuer);

                var user = _userRepository.GetUserById(userID);

                return Ok(user);
            }
            catch (Exception ex)
            {
                return Unauthorized();
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
