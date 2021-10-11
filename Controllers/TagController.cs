using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using todolistReactAsp.Connection;
using todolistReactAsp.Models;

namespace todolistReactAsp.Controllers
{

    [Route("api/tags")]
    public class TagController : ControllerBase
    {
        private readonly ApplicationContext _context;
        public TagController(ApplicationContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult Get()
        {
            var query = _context.Tag.AsQueryable();
            return Ok(query);
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] Tag tag)
        {
            await _context.AddAsync(tag);
            await _context.SaveChangesAsync();
            return CreatedAtRoute(new { id = tag.Id }, tag);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var _tag = await _context.Tag.FirstOrDefaultAsync(n => n.Id == id);
            if (_tag != null)
            {
                _context.Tag.Remove(_tag);
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