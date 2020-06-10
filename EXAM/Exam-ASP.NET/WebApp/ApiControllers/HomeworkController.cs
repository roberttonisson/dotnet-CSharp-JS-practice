using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DAL.App.EF;
using DAL.App.EF.Helpers;
using Domain;
using Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;
using PublicApi.DTO.v1;

namespace WebApp.ApiControllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class HomeworkController : ControllerBase
    {
        private readonly AppDbContext _context;

        public HomeworkController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Homework
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Homework>>> GetHomework()
        {
            return await _context.Homework.ToListAsync();
        }
        
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<HomeworkDTO>>> GetTeacherHomework(string id)
        {
            var homework = await _context.Homework
                .Where(hw => hw.CourseId == Guid.Parse(id))
                .Select(hw => new HomeworkDTO()
                {
                    Description = hw.Description,
                    Id = hw.Id.ToString(),
                    Title = hw.Title
                })
                .ToListAsync();

            return Ok(homework);
        }
        
        [HttpGet("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = RoleNames.RoleStudent)]
        public async Task<ActionResult<IEnumerable<HomeworkDTO>>> GetStudentHomework(string id)
        {
            var guidId = Guid.Parse(id);
            var homework = 
                await _context.Homework
                .Where(hw => hw.CourseId == guidId)
                .Select(hw => new HomeworkDTO()
                {
                    Description = hw.Description,
                    Id = hw.Id.ToString(),
                    Title = hw.Title,
                    CourseId = hw.CourseId.ToString()
                })
                .ToListAsync();
            
            
            return homework;
        }
        
        
        // GET: api/Homework/5
        [HttpGet("{id}")]
        public async Task<ActionResult<HomeworkDTO>> GetHomework(string id)
        {
            var homework = await _context.Homework.FindAsync(Guid.Parse(id));
            var dto = new HomeworkDTO()
            {
                Description = homework.Description,
                Id = homework.Id.ToString(),
                Title = homework.Title,
                CourseId = homework.CourseId.ToString()
            };
            if (homework == null)
            {
                return NotFound();
            }

            return Ok(dto);
        }

        // PUT: api/Homework/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHomework(string id, HomeworkDTO homeworkDTO)
        {
            var hw = await _context.Homework.FirstOrDefaultAsync(s => s.Id == Guid.Parse(id));

            if (hw == null)
            {
                return NotFound();
            }

            hw.Title = homeworkDTO.Title;
            hw.Description = homeworkDTO.Description;

            //_context.Entry(homework).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                
            }

            return NoContent();
        }

        // POST: api/Homework
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Homework>> PostHomework(HomeworkDTO homeworkDTO)
        {
            
            _context.Homework.Add(new Homework()
            {
                Description = homeworkDTO.Description,
                Title = homeworkDTO.Title,
                CourseId = Guid.Parse(homeworkDTO.CourseId)
            });
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetHomework", new { id = homeworkDTO }, homeworkDTO);
        }

        // DELETE: api/Homework/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Homework>> DeleteHomework(string id)
        {
            var homework = await _context.Homework.FindAsync(Guid.Parse(id));
            if (homework == null)
            {
                return NotFound();
            }

            _context.Homework.Remove(homework);
            await _context.SaveChangesAsync();

            return homework;
        }

        private bool HomeworkExists(Guid id)
        {
            return _context.Homework.Any(e => e.Id == id);
        }
    }
}
