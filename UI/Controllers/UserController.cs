using Microsoft.AspNetCore.Mvc;
using ORM.Dapper.Entities;
using ORM.Dapper.Interfaces;
using System.Threading.Tasks;

namespace UI.Controllers
{
    [ApiController]
    [Route("api/user")]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet]
        [Route("all")]
        public async Task<IActionResult> GetUser()
        {
            var users = await _userRepository.GetAllWithMessages();

            return Ok(users);
        }

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> CreateUser(User user)
        {
            await _userRepository.Add(user);

            return StatusCode(201);
        }
    }
}
