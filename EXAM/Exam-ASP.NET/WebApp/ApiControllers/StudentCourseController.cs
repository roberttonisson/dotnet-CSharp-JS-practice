using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DAL.App.EF;
using Domain;
using Domain.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using PublicApi.DTO.v1;
using WebApp.Areas.Identity.Pages.Account.Manage;

namespace WebApp.ApiControllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class StudentCourseController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly UserManager<AppUser> _userManager;

        public StudentCourseController(AppDbContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: api/StudentCourse
        [HttpGet("{courseid}")]
        public async Task<ActionResult<IEnumerable<StudentCourseDTO>>> GetStudentCourses(string courseid)
        {
            var studentCourses = await _context
                .StudentCourses
                .Include(s => s.AppUser)
                .Include(s => s.Course)
                .Where(ss => ss.CourseId == Guid.Parse(courseid))
                .Select(ss => new StudentCourseDTO()
                {
                    Accepted = ss.Accepted,
                    Grade = ss.Grade.ToString(),
                    Id = ss.Id.ToString(),
                    AppUser = new UserDTO()
                    {
                        Email = ss.AppUser.Email,
                        Id = ss.AppUserId.ToString(),
                        FirstName = ss.AppUser.FirstName,
                        LastName = ss.AppUser.LastName
                    },
                    Course = new CourseDTO()
                    {
                        Code = ss.Course.Code,
                        Description = ss.Course.Description,
                        Id = ss.CourseId.ToString(),
                        Name = ss.Course.Name,
                        Semester = ss.Course.Semester.ToString(),
                        Year = ss.Course.Year,
                        ECTS = ss.Course.ECTS
                    }
                }).ToListAsync();
            
            return Ok(studentCourses);
        }

        // GET: api/StudentCourse/5
        [HttpGet("{id}")]
        public async Task<ActionResult<StudentCourseDTO>> GetStudentCourse(string id)
        {
            var studentCourse =  _context.StudentCourses
                    .Include(s => s.Course)
                    .Include(s => s.AppUser)
                    .Where(s => s.Id == Guid.Parse(id))
                    .Select(s => new StudentCourseDTO()
                    {
                        Accepted = s.Accepted,
                        Grade = s.Grade.ToString(),
                        Id = s.Id.ToString(),
                        AppUser = new UserDTO()
                        {
                            Email = s.AppUser.Email,
                            Id = s.AppUser.Id.ToString(),
                            FirstName = s.AppUser.FirstName,
                            LastName = s.AppUser.LastName
                        },
                        Course = new CourseDTO()
                        {
                            Code = s.Course.Code,
                            Description = s.Course.Description,
                            Id = s.Course.Id.ToString(),
                            Name = s.Course.Name,
                            Semester = s.Course.Semester.ToString(),
                            Year = s.Course.Year,
                            ECTS = s.Course.ECTS
                        }
                    })
                ;

            if (studentCourse == null)
            {
                return NotFound();
            }

            return Ok(studentCourse);
        }

        // PUT: api/StudentCourse/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(string id, StudentCourseDTO studentCourseDTO)
        {
            if (id != studentCourseDTO.Id)
            {
                return BadRequest();
            }

            var studentCourse =
                await _context.StudentCourses.FirstOrDefaultAsync(s => s.Id == Guid.Parse(studentCourseDTO.Id));

            if (studentCourse == null)
            {
                return NotFound();
            }

            studentCourse.Accepted = studentCourseDTO.Accepted;
            if (studentCourseDTO.Grade != null) studentCourse.Grade = int.Parse(studentCourseDTO.Grade);

            //_context.Entry(studentCourse).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                
            }

            return NoContent();
        }

        // POST: api/StudentCourse
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<StudentCourse>> PostStudentCourse(StudentCourseDTO dto)
        {
            var user = await _userManager.FindByEmailAsync(dto.AppUser.Email);
            var ss = new StudentCourse();
            ss.AppUserId = user.Id;
            ss.CourseId = Guid.Parse(dto.Course.Id);
            _context.StudentCourses.Add(ss);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/StudentCourse/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<StudentCourse>> DeleteStudentCourse(Guid id)
        {
            var studentCourse = await _context.StudentCourses.FindAsync(id);
            if (studentCourse == null)
            {
                return NotFound();
            }

            _context.StudentCourses.Remove(studentCourse);
            await _context.SaveChangesAsync();

            return studentCourse;
        }

        private bool StudentCourseExists(Guid id)
        {
            return _context.StudentCourses.Any(e => e.Id == id);
        }
        
    }
}
