using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using todolistReactAsp.Connection;
using todolistReactAsp.Models;

namespace todolistReactAsp.Controllers
{
    [Route("api/jobs")]
    public class JobController : ControllerBase
    {
        private readonly ApplicationContext _context;
        public JobController(ApplicationContext context)
        {
            _context = context;
        }

        [HttpGet("{id}/{category}/{tag}")]
        public IActionResult Get(int id, int category, int tag)
        {
            if (category == 0 && tag == 0)
            {
                var query = _context.Job.Where(n => n.UserId == id).AsQueryable()
                .OrderBy(n => n.Id)
                .Include(n => n.TagJob)
                .Select(x => new
                {
                    x.Id,
                    x.Important,
                    x.Time,
                    x.Name,
                    x.CategoryId,
                    x.File,
                    x.UserId,
                    Tag = x.TagJob.Select(m => m.Tag).ToList()
                });
                return Ok(query);
            }
            else if (category == 0 && tag != 0)
            {
                var query = _context.Job.Where(n => n.UserId == id && n.TagJob.Any(q => q.TagId == tag)).AsQueryable()
                .OrderBy(n => n.Id)
                .Include(n => n.TagJob)
                .Select(x => new
                {
                    x.Id,
                    x.Important,
                    x.Time,
                    x.Name,
                    x.CategoryId,
                    x.File,
                    x.UserId,
                    Tag = x.TagJob.Select(m => m.Tag).ToList()
                });
                return Ok(query);
            }
            else if (category != 0 && tag == 0)
            {
                var query = _context.Job.Where(n => n.UserId == id && n.CategoryId == category).AsQueryable()
                .OrderBy(n => n.Id)
                .Include(n => n.TagJob)
                .Select(x => new
                {
                    x.Id,
                    x.Important,
                    x.Time,
                    x.Name,
                    x.CategoryId,
                    x.File,
                    x.UserId,
                    Tag = x.TagJob.Select(m => m.Tag).ToList()
                });
                return Ok(query);
            }
            else if (category != 0 && tag != 0)
            {
                var query = _context.Job.Where(n => n.UserId == id && n.CategoryId == category && n.TagJob.Any(q => q.TagId == tag)).AsQueryable()
                .OrderBy(n => n.Id)
                .Include(n => n.TagJob)
                .Select(x => new
                {
                    x.Id,
                    x.Important,
                    x.Time,
                    x.Name,
                    x.CategoryId,
                    x.File,
                    x.UserId,
                    Tag = x.TagJob.Select(m => m.Tag).ToList()
                });

                return Ok(query);
            }
            else
            {
                return Ok("ko");
            }
        }
        [HttpGet("getOne/{id}")]
        public IActionResult GeOne(int id)
        {
            var query = _context.Job.Where(n => n.Id == id).Include(n => n.TagJob)
            .Select(x => new
            {
                x.Id,
                x.Important,
                x.Time,
                x.Name,
                x.CategoryId,
                x.File,
                x.UserId,
                Tag = x.TagJob.Select(m => m.Tag).ToList()
            });
            return Ok(query);
        }
        public string getDay(string e)
        {
            return e.Length == 1 ? ("0" + e) : e;
        }
        [HttpGet("getJobToday/{id}")]
        public IActionResult GeJobToday(int id)
        {
            DateTime now = DateTime.Now;
            var date = now.Year + "-" + now.Month + "-" + getDay((now.Day).ToString());
            var query = _context.Job.Where(n => n.UserId == id && n.Time == date)
            .Select(x => new
            {
                x.Id,
                x.Name,
                x.Important,
                x.Time,
                x.UserId,
            });
            return Ok(query);
        }
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] Job job)
        {
            await _context.AddAsync(job);
            await _context.SaveChangesAsync();
            return CreatedAtRoute(new { id = job.Id }, job);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync([FromBody] Job job, int id)
        {
            var _student = await _context.Job.FirstOrDefaultAsync(n => n.Id == id);
            if (_student != null)
            {
                _student.Name = job.Name;
                _student.CategoryId = job.CategoryId;
                _student.Time = job.Time;
                _student.File = job.File;
                _student.Important = job.Important;
                await _context.SaveChangesAsync();
                return Ok("Success");
            }
            else
            {
                return Ok("Error");
            }
        }
        [HttpPut("important/{id}")]
        public async Task<IActionResult> PutImportantAsync([FromBody] Job job, int id)
        {
            var _student = await _context.Job.FirstOrDefaultAsync(n => n.Id == id);
            if (_student != null)
            {
                _student.Important = job.Important;
                await _context.SaveChangesAsync();
                return Ok("Success");
            }
            else
            {
                return Ok("Error");
            }
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var _student = await _context.Job.FirstOrDefaultAsync(n => n.Id == id);
            if (_student != null)
            {
                _context.Job.Remove(_student);
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