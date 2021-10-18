using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using todolistReactAsp.Connection;
using todolistReactAsp.Models;
namespace todolistReactAsp.Controllers
{
    [Route("api/tokenNotifications")]
    public class TokenNotificationController : ControllerBase
    {
        private readonly ApplicationContext _context;
        public TokenNotificationController(ApplicationContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var query = _context.User.Where(a => a.EmailConfirmed == true)
            .Include(n => n.TokenNotifications)
            .Select(x => new
            {
                x.Id,
                TokenNotification = x.TokenNotifications.Select(m => m.Token)
            }).AsQueryable();
            return Ok(query);
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] TokenNotification tokenNotification)
        {
            var _tokenNotification = await _context.TokenNotification.FirstOrDefaultAsync(n => n.Token == tokenNotification.Token && n.UserId == tokenNotification.UserId);
            if (_tokenNotification == null)
            {
                await _context.AddAsync(tokenNotification);
                await _context.SaveChangesAsync();
                return CreatedAtRoute(new { id = tokenNotification.Id }, tokenNotification);
            }
            return Ok("Error");
        }
        [HttpPost("delete")]
        public async Task<IActionResult> DeleteToken([FromBody] TokenNotification tokenNotification)
        {
            var _tokenNotification = await _context.TokenNotification.FirstOrDefaultAsync(n => n.Token == tokenNotification.Token && n.UserId == tokenNotification.UserId);
            if (_tokenNotification != null)
            {
                _context.TokenNotification.Remove(_tokenNotification);
                await _context.SaveChangesAsync();
                return Ok("Success");
            }
            return Ok("Error");
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var _tokenNotification = await _context.TokenNotification.FirstOrDefaultAsync(n => n.Id == id);
            if (_tokenNotification != null)
            {
                _context.TokenNotification.Remove(_tokenNotification);
                await _context.SaveChangesAsync();
                return Ok("Success");
            }
            else
            {
                return Ok("Error");
            }
        }
    }
}