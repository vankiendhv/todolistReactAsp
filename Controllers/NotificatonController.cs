using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using todolistReactAsp.Connection;
using todolistReactAsp.Models;

namespace todolistReactAsp.Controllers
{
    [Route("api/notification")]
    public class NotificatonController : ControllerBase
    {
        private readonly ApplicationContext _context;
        public NotificatonController(ApplicationContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] Notification notification)
        {
            await _context.AddAsync(notification);
            await _context.SaveChangesAsync();
            return CreatedAtRoute(new { id = notification.Id }, notification);
        }
        [HttpGet("{id}")]
        public IActionResult GetAsync(int id)
        {
            var query = _context.Notification.Where(n => n.userId == id).OrderByDescending(m => m.Id).Take(10);
            return Ok(query);
        }
        [HttpGet("getNumberNoActive/{id}")]
        public IActionResult GetNumberAsync(int id)
        {
            var query = _context.Notification.Where(n => n.userId == id && n.Status == 0);
            return Ok(query);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStatusAsync([FromBody] Notification notification, int id)
        {
            var _notification = await _context.Notification.FirstOrDefaultAsync(n => n.Id == id);
            if (_notification != null)
            {
                _notification.Status = notification.Status;
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