using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using todolistReactAsp.Connection;
using todolistReactAsp.Models;

namespace todolistReactAsp.Controllers
{
    [Authorize]
    [Route("api/categories")]
    public class CategoryController : ControllerBase
    {
        private readonly ApplicationContext _context;
        public CategoryController(ApplicationContext context)
        {
            _context = context;
        }
        [AllowAnonymous]
        [HttpGet]
        public IActionResult Get()
        {
            var query = _context.Category.AsQueryable();
            return Ok(query);
        }
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] Category category)
        {
            await _context.AddAsync(category);
            await _context.SaveChangesAsync();
            return CreatedAtRoute(new { id = category.Id }, category);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var _category = await _context.Category.FirstOrDefaultAsync(n => n.Id == id);
            if (_category != null)
            {
                _context.Category.Remove(_category);
                await _context.SaveChangesAsync();
                return Ok("Success");
            }
            else
            {
                return Ok(_category);
            }
        }
    }
}