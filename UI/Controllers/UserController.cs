using Microsoft.AspNetCore.Mvc;
using ORMs.Domain.Entities;
using System.Threading.Tasks;
using System;
//using ORM.Dapper.Common.Interfaces;
using ORM.ADO.NET.Common.Interfaces;
using System.Collections.Generic;

namespace UI.Controllers
{
    [ApiController]
    [Route("api/user")]
    public class UserController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        [Route("all")]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _unitOfWork.Users.GetAllWithMessages();

            return Ok(users);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var user = await _unitOfWork.Users.Get(id);

            return Ok(user);
        }

        [HttpGet]
        [Route("")]
        public async Task<IActionResult> Get(string username)
        {
            var user = await _unitOfWork.Users.GetWhereUsernameLike(username);

            return Ok(user);
        }

        [HttpPost]
        [Route("")]
        public async Task<IActionResult> Create(User user)
        {
            await _unitOfWork.Users.Create(user);
            await _unitOfWork.Save();

            return StatusCode(201);
        }

        [HttpPost]
        [Route("some")]
        public async Task<IActionResult> Create(IEnumerable<User> users)
        {
            await _unitOfWork.Users.CreateRange(users);
            await _unitOfWork.Save();

            return StatusCode(201);
        }

        [HttpPut]
        [Route("")]
        public async Task<IActionResult> Update(User user)
        {
            await _unitOfWork.Users.Update(user);
            await _unitOfWork.Save();

            return StatusCode(200);
        }

        [HttpPut]
        [Route("some")]
        public async Task<IActionResult> Update(IEnumerable<User> users)
        {
            await _unitOfWork.Users.UpdateRange(users);
            await _unitOfWork.Save();

            return StatusCode(200);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var user = await _unitOfWork.Users.Get(id);
            if (user == null)
            {
                return BadRequest();
            }

            await _unitOfWork.Users.Delete(user);
            await _unitOfWork.Save();

            return StatusCode(200);
        }

        [HttpDelete]
        [Route("some")]
        public async Task<IActionResult> Delete(IEnumerable<User> users)
        {
            await _unitOfWork.Users.DeleteRange(users);
            await _unitOfWork.Save();

            return StatusCode(200);
        }
    }
}
