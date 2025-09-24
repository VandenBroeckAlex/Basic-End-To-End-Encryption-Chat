using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
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

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet]
        [ProducesResponseType(200,Type = typeof(IEnumerable<User>))]
        public IActionResult GetUser()
        {
            var user = _userRepository.GetUsers();

            if (!ModelState.IsValid) 
            {
                return BadRequest(ModelState);
            }
            return Ok(user);
        }
    }
}
