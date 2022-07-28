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

namespace Jobs.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobsController : ControllerBase
    {

        private readonly IUserRepository _userRepository;
        private readonly IJobRepository _jobRepository;

        public JobsController(IJobRepository jobRepository, IUserRepository userRepository)
        {
            _jobRepository = jobRepository;
            _userRepository = userRepository;
        }

        // GET: api/Jobs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Job>>> GetJob()
        {
            var job = await _jobRepository.GetJobs();
            if (job == null)
            {
                return NotFound();
            }
            return Ok(job);
        }

        // GET: api/Jobs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Job>> GetJob(string id)
        {
            var job = await _jobRepository.GetJob(id);

            if (job == null)
            {
                return NotFound();
            }

            return job;
        }

        // PUT: api/Jobs/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutJob(string id, Job job)
        {
            if (id != job.Id)
            {
                return BadRequest();
            }

            try
            {
                await _jobRepository.CreateJob(job);
            }
            catch (Exception)
            {
                throw;
            }

            return NoContent();
        }

        // POST: api/Jobs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Job>> PostJob(Job job)
        {
            var user = _userRepository.GetUser(job.Owner).Result;
            if (user == null)
            {
                return Problem("Wrong phone number!");
            }
            if (user.RightToPublish == 0)
            {
                return Problem("User has no right to publish.");
            }           
            user.RightToPublish--;

            await _jobRepository.UpdateJob(job);
            await _userRepository.UpdateUser(user);

            return CreatedAtAction("GetJob", new { id = job.Id }, job);
        }

        // DELETE: api/Jobs/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteJob(string id)
        {
            var job = await _jobRepository.GetJob(id);
            if (job == null)
            {
                return NotFound();
            }

            await _jobRepository.DeleteJob(id);

            return NoContent();
        }
    }
}
