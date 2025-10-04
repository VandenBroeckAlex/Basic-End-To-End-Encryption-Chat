using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using stamp_back.Dto;
using stamp_back.Interfaces;
using stamp_back.Models;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

namespace stamp_back.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        // GET 
        public UserController(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<User>))]
        public IActionResult GetUser()
        {
            var user = _mapper.Map<List<UserDto>>(_userRepository.GetUsers());

            
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(user);
        }

        [HttpGet("id/{userid:guid}")]
        [ProducesResponseType(200, Type = typeof(User))]
        [ProducesResponseType(400)]
        public IActionResult GetUser(Guid userid) 
        {
            if (!_userRepository.UserExist(userid))
            {
                return NotFound();
            }
            var user = _mapper.Map<UserDto>(_userRepository.GetUserById(userid));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(user);
        }

        [HttpGet("name/{userName}")]
        [ProducesResponseType(200, Type = typeof(User))]
        [ProducesResponseType(400)]
        public IActionResult GetUser(string userName) 
        { 
            var user = _mapper.Map<UserDto>(_userRepository.GetUserByName(userName));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(user);
        }

        [HttpGet("email/{email}")]
        [ProducesResponseType(2000, Type = typeof(User))]
        [ProducesResponseType(400)] 
        public IActionResult GetUserByEmail(string email) 
        {
            var user = _mapper.Map<UserDto>(_userRepository.GetUserByEmail(email));
            if(!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(user);
        }
        //POST
        [HttpPost("")]
        public IActionResult Register(UserDto.UserRegisterDto _user)
        {
            var user = new User
            {
                UserName = _user.UserName,
                Email = _user.Email,
                Password = BCrypt.Net.BCrypt.HashPassword(_user.Password),
            };

            
 
            return Created("sucess", _userRepository.PostUser(user));
        }
    }
}
