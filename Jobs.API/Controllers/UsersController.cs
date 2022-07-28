using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Jobs.API.Data;
using Jobs.API.Entities;
using Jobs.API.Interface;
using Jobs.API.Models;

namespace Jobs.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public UsersController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUser()
        {
            var user = await _userRepository.GetUsers();
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(string id)
        {
            var user = await _userRepository.GetUser(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        // PUT: api/Users/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(string id, User user)
        {
            if (id != user.Id)
            {
                return BadRequest();
            }

            try
            {
                await _userRepository.UpdateUser(user);
            }
            catch (DbUpdateConcurrencyException)
            {
                //if (!_userRepository.UserExists(id))
                //{
                //    return NotFound();
                //}
                //else
                //{
                //    throw;
                //}
            }

            return NoContent();
        }

        // POST: api/Users
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<User>> PostUser(User user)
        {
            try
            {
                var userExist = _userRepository.GetUser(user.Phone).Result;
                if (userExist != null)
                    return Conflict();
                await _userRepository.CreateUser(user);
            }
            catch (DbUpdateException)
            {
                //if (_userRepository.UserExists(user.Phone))
                //{
                //    return Conflict();
                //}
                //else
                //{
                //    throw;
                //}
            }

            return CreatedAtAction("GetUser", new { id = user.Phone }, user);
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            var user = await _userRepository.GetUser(id);
            if (user == null)
            {
                return NotFound();
            }

            await _userRepository.DeleteUser(id);

            return NoContent();
        }
    }
}
