using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ORM.EntityFrameworkCore;
using ORMs.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UI.Controllers
{
    [ApiController]
    [Route("api/entity-framework/user")]
    public class EntityFrameworkController : Controller
    {
        private readonly MessengerDbContext _dbContext;

        public EntityFrameworkController(MessengerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        [Route("all")]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _dbContext.Users
                .Include(x => x.Folders)
                .ThenInclude(x => x.Messages)
                .AsNoTracking()
                .ToListAsync();

            users.ForEach(x =>
            {
                foreach (var folder in x.Folders)
                {
                    folder.Users = null;
                    folder.UserFolders = null;
                    foreach (var message in folder.Messages)
                    {
                        message.Folder = null;
                    }
                }
                foreach (var userFolder in x.UserFolders)
                {
                    userFolder.User = null;
                    userFolder.Folder = null;
                }
            });

            return Ok(users);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var user = await _dbContext.Users
                .FindAsync(id);

            return Ok(user);
        }

        [HttpGet]
        [Route("")]
        public async Task<IActionResult> Get(string username)
        {
            var users = await _dbContext.Users
                .Where(x => EF.Functions.Like(x.Username, "%" + username + "%"))
                .AsNoTracking()
                .ToListAsync();

            return Ok(users);
        }

        [HttpPost]
        [Route("")]
        public async Task<IActionResult> Create(User user)
        {
            await _dbContext.Users.AddAsync(user);
            await _dbContext.SaveChangesAsync();

            return StatusCode(201);
        }

        [HttpPost]
        [Route("some")]
        public async Task<IActionResult> Create(IEnumerable<User> users)
        {
            await _dbContext.Users.AddRangeAsync(users);
            await _dbContext.SaveChangesAsync();

            return StatusCode(201);
        }

        [HttpPut]
        [Route("")]
        public async Task<IActionResult> Update(User user)
        {
            _dbContext.Update(user);
            await _dbContext.SaveChangesAsync();

            return StatusCode(200);
        }

        [HttpPut]
        [Route("some")]
        public async Task<IActionResult> Update(IEnumerable<User> users)
        {
            _dbContext.Users.UpdateRange(users);
            await _dbContext.SaveChangesAsync();

            return StatusCode(200);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var user = await _dbContext.Users.FindAsync(id);
            if (user == null)
            {
                return BadRequest();
            }

            _dbContext.Users.Remove(user);
            await _dbContext.SaveChangesAsync();

            return StatusCode(200);
        }

        [HttpDelete]
        [Route("some")]
        public async Task<IActionResult> Delete(IEnumerable<User> users)
        {
            _dbContext.Users.RemoveRange(users);
            await _dbContext.SaveChangesAsync();

            return StatusCode(200);
        }
    }
}