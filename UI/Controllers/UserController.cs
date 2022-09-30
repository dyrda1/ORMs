using Microsoft.AspNetCore.Mvc;
using ORMs.Domain.Entities;
using ORM.Dapper.Interfaces;
using System.Threading.Tasks;
using System;

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
        public async Task<IActionResult> GetUsers()
        {
            var users = await _userRepository.GetAllWithComments();

            return Ok(users);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var user = await _userRepository.Get(id);

            return Ok(user);
        }

        [HttpGet]
        [Route("")]
        public async Task<IActionResult> Get(string username)
        {
            var user = await _userRepository.GetWhereUsernameLike(username);

            return Ok(user);
        }

        [HttpPost]
        [Route("")]
        public async Task<IActionResult> Create(User user)
        {
            await _userRepository.Add(user);

            return StatusCode(201);
        }

        [HttpPut]
        [Route("")]
        public async Task<IActionResult> Update(User user)
        {
            await _userRepository.Update(user);

            return StatusCode(200);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _userRepository.Delete(id);

            return StatusCode(200);
        }
    }
}
