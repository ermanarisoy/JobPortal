
using Jobs.API.Entities;
using Jobs.API.Interface;
using Microsoft.AspNetCore.Mvc;
namespace Jobs.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BadWordsController : ControllerBase
    {
        private readonly IBadWordRepository _badWordRepository;

        public BadWordsController(IBadWordRepository badWordRepository)
        {
            _badWordRepository = badWordRepository;
        }

        // GET: api/BadWords
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BadWord>>> GetBadWord()
        {
            var badWord = await _badWordRepository.GetBadWords();
            if (badWord == null)
            {
                return NotFound();
            }
            return Ok(badWord);
        }

        // GET: api/BadWords/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BadWord>> GetBadWord(string id)
        {
            var badWord = await _badWordRepository.GetBadWord(id);

            if (badWord == null)
            {
                return NotFound();
            }

            return badWord;
        }

        // PUT: api/BadWords/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBadWord(string id, BadWord badWord)
        {
            if (id != badWord.Id)
            {
                return BadRequest();
            }

            try
            {
                await _badWordRepository.UpdateBadWord(badWord);
            }
            catch (Exception)
            {
                throw;
            }

            return NoContent();
        }

        // POST: api/BadWords
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<BadWord>> PostUBadWord(BadWord badWord)
        {
            try
            {
                var badWordExist = _badWordRepository.GetBadWordByWord(badWord.Word).Result;
                if (badWordExist != null)
                    return Conflict();
                await _badWordRepository.CreateBadWord(badWord);
            }
            catch (Exception)
            {
                throw;
            }

            return CreatedAtAction("GetBadWord", new { id = badWord.Word }, badWord);
        }

        // DELETE: api/BadWords/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBadWord(string id)
        {
            var badWord = await _badWordRepository.GetBadWord(id);
            if (badWord == null)
            {
                return NotFound();
            }

            await _badWordRepository.DeleteBadWord(id);

            return NoContent();
        }
    }
}
