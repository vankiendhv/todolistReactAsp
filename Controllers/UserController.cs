using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using todolistReactAsp.Connection;
using todolistReactAsp.Models;

namespace todolistReactAsp.Controllers
{
    [Route("api/users")]
    public class UserController : ControllerBase
    {
        private readonly ApplicationContext _context;
        public UserController(ApplicationContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult Get()
        {
            var query = _context.User.AsQueryable();
            return Ok(query);
        }
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] User user)
        {
            await _context.AddAsync(user);
            await _context.SaveChangesAsync();
            return CreatedAtRoute(new { id = user.Id }, user);
        }
        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync([FromBody] User user)
        {
            var _user = await _context.User.FirstOrDefaultAsync(n => n.UserName == user.UserName && n.Password == user.Password);
            if (_user != null)
            {
                return Ok("Success " + _user.Id + " " + _user.Name);
            }
            else
            {
                return Ok("Error");
            }
        }

    }
}