using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using todolistReactAsp.Connection;
using todolistReactAsp.Models;

namespace todolistReactAsp.Controllers
{
    [Route("api/tagJobs")]
    public class TagJobsController : ControllerBase
    {
        private readonly ApplicationContext _context;
        public TagJobsController(ApplicationContext context)
        {
            _context = context;
        }
        [HttpGet("{id}")]
        public IActionResult GetTag(int id)
        {
            var query = _context.TagJob.Where(e => e.JobId == id);
            return Ok(query);
        }
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] TagJob tagJob)
        {
            await _context.AddAsync(tagJob);
            await _context.SaveChangesAsync();
            return CreatedAtRoute(new { id = tagJob.Id }, tagJob);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            // var _tag = _context.TagJob.FirstOrDefaultAsync(n => n.JobId == id).Result;
            var _tag = _context.TagJob.Where(n => n.JobId == id);
            if (_tag != null)
            {
                _context.TagJob.RemoveRange(_tag);
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