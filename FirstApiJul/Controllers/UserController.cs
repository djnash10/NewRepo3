using Data;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FirstApiJul.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _repository;
        public UserController(IUserRepository repository)
        {
            _repository = repository;
        }

        // GET: api/<User>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Users>>> GetUsers()
        {
            return Ok(await _repository.GetAll());
        }

        // GET api/<User>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Users>> GetUser(int id)
        {
            var user = await _repository.GetDetails(id);

            if (user == null)
                return NotFound();

            return user;
        }

        // POST api/<User>
        [HttpPost]
        public async Task<ActionResult<Users>> PostUser(Users user)
        {
            await _repository.Insert(user);

            return CreatedAtAction("GetUsers", new { id = user.Id }, user);
        }

        // PUT api/<User>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, Users user)
        {
            if (id != user.Id)
                return BadRequest();

            var userInDb = await _repository.GetDetails(id);

            if (userInDb == null)
                return NotFound();

            await _repository.Update(user);

            return NoContent();
        }

        // DELETE api/<User>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Users>> DeleteUser(int id)
        {
            var user = await _repository.GetDetails(id);

            if (user == null)
                return NotFound();

            await _repository.Delete(id);

            return user;
        }
    }
}
