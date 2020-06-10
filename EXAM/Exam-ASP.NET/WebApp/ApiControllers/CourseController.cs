using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DAL.App.EF;
using DAL.App.EF.Helpers;
using Domain;
using Domain.Identity;
using Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using PublicApi.DTO.v1;

namespace WebApp.ApiControllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class CoursesController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly UserManager<AppUser> _userManager;

        public CoursesController(AppDbContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: api/Courses
        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CourseDTO>>> GetCourses()
        {
            return Ok(await _context.Courses
                .Include(s => s.AppUser)
                .Include(s => s.StudentCourses)
                .ThenInclude(s => s.AppUser)
                .Select(s => new CourseDTO()
                {
                    Code = s.Code,
                    Description = s.Description,
                    Id = s.Id.ToString(),
                    Name = s.Name,
                    Semester = s.Semester.ToString(),
                    User = s.AppUser == null
                        ? null
                        : new UserDTO()
                        {
                            Email = s.AppUser.Email,
                            Id = s.AppUser.Id.ToString(),
                            FirstName = s.AppUser.FirstName,
                            LastName = s.AppUser.LastName
                        },
                    Year = s.Year,
                    ECTS = s.ECTS,
                    StudentCourses = s.StudentCourses.Select(sb => new StudentCourseDTO()
                    {
                        Accepted = sb.Accepted,
                        Grade = sb.Grade.ToString(),
                        Id = sb.Id.ToString(),
                        AppUser = new UserDTO()
                        {
                            Email = sb.AppUser.Email,
                            Id = sb.AppUser.Id.ToString(),
                            FirstName = sb.AppUser.FirstName,
                            LastName = sb.AppUser.LastName
                        }
                    }).ToList()
                }).ToListAsync()
            );
        }
        
        [HttpGet("{email}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = RoleNames.RoleStudent)]
        public async Task<ActionResult<IEnumerable<StudentCourseDTO>>> GetStudentCourses(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return NotFound();
            }

            var studentCourses =
                    await _context.StudentCourses
                        .Include(s => s.Course)
                        .Where(s => s.AppUserId == user.Id && s.Accepted == true)
                        .Select(s => new StudentCourseDTO()
                        {
                            Accepted = s.Accepted,
                            Grade = s.Grade.ToString(),
                            Id = s.Id.ToString(),
                            Course = new CourseDTO()
                            {
                                Code = s.Course.Code,
                                Description = s.Course.Description,
                                Id = s.CourseId.ToString(),
                                Name = s.Course.Name,
                                Semester = s.Course.Semester.ToString(),
                                Year = s.Course.Year,
                                ECTS = s.Course.ECTS
                            }
                        })
                .ToListAsync();

            return Ok(studentCourses);
        }

        [HttpGet("{email}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = RoleNames.RoleTeacher)]
        public async Task<ActionResult<IEnumerable<CourseDTO>>> GetTeacherCourses(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user == null)
            {
                return NotFound();
            }

            var teacherCourses =
                await _context.Courses.Include(s => s.AppUser)
                    .Where(s => s.AppUserId == user.Id)
                    .Select(s => new CourseDTO()
                    {
                        Code = s.Code,
                        Description = s.Description,
                        Id = s.Id.ToString(),
                        Name = s.Name,
                        Semester = s.Semester.ToString(),
                        Year = s.Year,
                        ECTS = s.ECTS,
                        User = new UserDTO()
                        {
                            Email = s.AppUser.Email,
                            Id = s.AppUserId.ToString(),
                            FirstName = s.AppUser.FirstName,
                            LastName = s.AppUser.LastName
                        }
                    })
                    .ToListAsync();

            return Ok(teacherCourses);
        }


        // GET: api/Courses/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Course>> GetCourse(Guid id)
        {
            var course = await _context.Courses.FindAsync(id);

            if (course == null)
            {
                return NotFound();
            }

            return course;
        }

        // PUT: api/Courses/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = RoleNames.RoleAdmin)]
        public async Task<IActionResult> PutCourse(Guid id, Course course)
        {
            if (id != course.Id)
            {
                return BadRequest();
            }

            _context.Entry(course).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CourseExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Courses
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = RoleNames.RoleAdmin)]
        public async Task<ActionResult<Course>> PostCourse(Course course)
        {
            _context.Courses.Add(course);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCourse", new {id = course.Id}, course);
        }

        // DELETE: api/Courses/5
        [HttpDelete("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = RoleNames.RoleAdmin)]
        public async Task<ActionResult<Course>> DeleteCourse(Guid id)
        {
            var course = await _context.Courses.FindAsync(id);
            if (course == null)
            {
                return NotFound();
            }

            _context.Courses.Remove(course);
            await _context.SaveChangesAsync();

            return course;
        }

        private bool CourseExists(Guid id)
        {
            return _context.Courses.Any(e => e.Id == id);
        }
    }
}
