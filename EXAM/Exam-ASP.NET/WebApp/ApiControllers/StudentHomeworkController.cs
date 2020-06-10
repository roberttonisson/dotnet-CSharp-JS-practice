using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
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
    public class StudentHomeworkController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly UserManager<AppUser> _userManager;

        public StudentHomeworkController(AppDbContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: api/StudentHomework
        [HttpGet]
        public async Task<ActionResult<IEnumerable<StudentHomework>>> GetStudentHomework()
        {
            return await _context.StudentHomework.ToListAsync();
        }
        
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = RoleNames.RoleStudent)]
        [HttpGet("{email}/{id}")]
        public async Task<ActionResult<StudentHomeworkDTO>> GetStudentIndex(string id, string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return NotFound();
            }

            var shw = await _context.StudentHomework
                          .Include(s => s.Homework)
                          .Where(sh =>
                              sh.HomeworkId == Guid.Parse(id) && sh.AppUserId == user.Id)
                          .Select(s => new StudentHomeworkDTO()
                          {
                              Grade = s.Grade.ToString(),
                              Id = s.Id.ToString(),
                              StudentAnswer = s.StudentAnswer,
                              Homework = new HomeworkDTO()
                              {
                                  Description = s.Homework.Description,
                                  Id = s.HomeworkId.ToString(),
                                  Title = s.Homework.Title,
                                  CourseId = s.Homework.CourseId.ToString()
                              },
                              AppUser = new UserDTO()
                              {
                                  Email = user.Email,
                                  Id = user.Id.ToString(),
                                  FirstName = user.FirstName,
                                  LastName = user.LastName
                              }
                          })
                          .FirstOrDefaultAsync()
                      ??
                      new StudentHomeworkDTO()
                      {
                          Homework = await _context.Homework.Where(h => h.Id == Guid.Parse(id))
                              .Select(s => new HomeworkDTO()
                              {
                                  Description = s.Description,
                                  Id = s.Id.ToString(),
                                  Title = s.Title,
                              })
                              .FirstOrDefaultAsync(),
                          AppUser = new UserDTO()
                          {
                              Email = user.Email,
                              Id = user.Id.ToString(),
                              FirstName = user.FirstName,
                              LastName = user.LastName
                          }
                      };

            return Ok(shw);
        }
        
        
        
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = RoleNames.RoleTeacher)]
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<StudentHomeworkDTO>>> GetTeacherIndex(string id)
        {
            var shw = await _context.StudentHomework
                .Include(s => s.AppUser)
                .Where(s => s.HomeworkId == Guid.Parse(id))
                .Select(s => new StudentHomeworkDTO()
                {
                    Grade = s.Grade.ToString(),
                    Id = s.Id.ToString(),
                    AppUser = new UserDTO()
                    {
                        Email = s.AppUser.Email,
                        Id = s.AppUserId.ToString(),
                        FirstName = s.AppUser.FirstName,
                        LastName = s.AppUser.LastName
                    },
                    StudentAnswer = s.StudentAnswer
                })
                .ToListAsync();

            return Ok(shw);
        }
        
       //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = RoleNames.RoleTeacher)]
        [HttpGet("homepage")]
        public ActionResult<IEnumerable<StudentCourseDTO>> HomeIndex()
        {
            var x =  _context.StudentCourses
                .Include(x => x.AppUser)
                .Include(x => x.Course)
                .ThenInclude(x => x!.Homework)
                .ThenInclude(x => x.StudentHomework)
                .Where(x => x.AppUserId == User.UserGuidId())
                .ToList();

            var dto = new List<StudentCourseDTO>();
            foreach (var ss in x)
            {
               dto.Add(new StudentCourseDTO
               {
                   Id = ss.Id.ToString(),
                   AppUserId = ss.AppUserId.ToString(),
                   AppUser = new UserDTO
                   {
                       Id = ss.AppUserId.ToString(),
                       FirstName = ss.AppUser!.FirstName,
                       LastName = ss.AppUser!.LastName,
                       Email = ss.AppUser!.Email
                   },
                   Course = new CourseDTO
                   {
                       Id = ss.Course!.Id.ToString(),
                       Name = ss.Course.Name,
                       Code = ss.Course.Code,
                       ECTS = ss.Course.ECTS,
                       Semester = ss.Course.Semester.ToString(),
                       Year = ss.Course.Year,
                       Description = ss.Course.Description,
                       Homework = getHomeWorkDTO(ss.Course.Homework!)
                   },
                   Accepted = ss.Accepted,
                   Grade = ss.Grade.ToString()
               }); 
            }
            return Ok(dto);
        }
        
        /*Helper method*/
        public ICollection<HomeworkDTO> getHomeWorkDTO(IEnumerable<Homework> hw)
        {
            var ret =  new List<HomeworkDTO>();
            foreach (var x in hw)
            {
                ret.Add(new HomeworkDTO
                {
                    Id = x.Id.ToString(),
                    CourseId = x.CourseId.ToString(),
                    Title = x.Title,
                    Description = x.Description,
                    Deadline = x.Deadline,
                    StudentHomework = getStudentHomeworkDTO(x.StudentHomework!)
                });
            }

            return ret;
        }
        /*HELPER*/
        public ICollection<StudentHomeworkDTO> getStudentHomeworkDTO(IEnumerable<StudentHomework> shw)
        {
            var ret =  new List<StudentHomeworkDTO>();
            foreach (var x in shw)
            {
                ret.Add(new StudentHomeworkDTO
                {
                    Id = x.Id.ToString(),
                    AppUserId = x.AppUserId.ToString(),
                    Grade = x.Grade.ToString(),
                    GradedAt = x.GradedAt,
                    StudentAnswer = x.StudentAnswer
                });
            }

            return ret;
        }
        


        // GET: api/StudentHomework/5
        [HttpGet("{id}")]
        public async Task<ActionResult<StudentHomeworkDTO>> GetStudentHomework(string id)
        {
            var hw = await _context
                .StudentHomework
                .Include(s => s.Homework)
                .Include(s => s.AppUser)
                .FirstOrDefaultAsync(s => s.Id == Guid.Parse(id));

            if (hw == null)
            {
                return NotFound();
            }

            var dto = new StudentHomeworkDTO()
            {
                Id = hw.Id.ToString(),
                Grade = hw.Grade.ToString(),
                Homework = new HomeworkDTO()
                {
                    Description = hw.Homework.Description,
                    Id = hw.Homework.Id.ToString(),
                    Title = hw.Homework.Title,
                },
                AppUser = new UserDTO()
                {
                    Email = hw.AppUser.Email,
                    Id = hw.AppUserId.ToString(),
                    FirstName = hw.AppUser.FirstName,
                    LastName = hw.AppUser.LastName
                },
                StudentAnswer = hw.StudentAnswer
            };

            return Ok(dto);
        }

        // PUT: api/StudentHomework/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStudentHomework(string id, StudentHomeworkDTO dto)
        {
            var shw = await _context.StudentHomework.FirstOrDefaultAsync(s => s.Id == Guid.Parse(id));

            if (shw == null)
            {
                return NotFound();
            }

            Console.WriteLine(dto.Grade);
            shw.Grade = int.Parse(dto.Grade);
            shw.StudentAnswer = dto.StudentAnswer;
            //_context.Entry(studentHomework).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
            }

            return NoContent();
        }

        // POST: api/StudentHomework
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<StudentHomework>> PostStudentHomework(StudentHomeworkDTO studentHomework)
        {
            _context.StudentHomework.Add(new StudentHomework()
            {
                Id = Guid.NewGuid(),
                HomeworkId = Guid.Parse(studentHomework.Homework.Id),
                AppUserId = Guid.Parse(studentHomework.AppUser.Id),
                StudentAnswer = studentHomework.StudentAnswer
            });
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetStudentHomework", new {id = studentHomework.Id}, studentHomework);
        }

        // DELETE: api/StudentHomework/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<StudentHomework>> DeleteStudentHomework(Guid id)
        {
            var studentHomework = await _context.StudentHomework.FindAsync(id);
            if (studentHomework == null)
            {
                return NotFound();
            }

            _context.StudentHomework.Remove(studentHomework);
            await _context.SaveChangesAsync();

            return studentHomework;
        }

        private bool StudentHomeworkExists(Guid id)
        {
            return _context.StudentHomework.Any(e => e.Id == id);
        }
    }
}