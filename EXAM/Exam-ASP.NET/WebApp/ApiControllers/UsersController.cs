using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL.App.EF;
using DAL.App.EF.Helpers;
using Domain;
using Domain.Identity;
using Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PublicApi.DTO.v1;

namespace WebApp.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController: Controller
    {
        
        private readonly AppDbContext _context;
        private readonly UserManager<AppUser> _userManager;

        public UsersController(AppDbContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDTO>>> Teacherss()
        {
            var users = await _userManager.Users.ToListAsync();
            var usersDTO = users.Where(IsTeacher).Select(u => new UserDTO()
            {
                Email = u.Email,
                Id = u.Id.ToString(),
                FirstName = u.FirstName,
                LastName = u.LastName
            }).ToList();
            
            return Ok(usersDTO);
        }
        


        public bool IsTeacher(AppUser user)
        {
            var isTeacher = _userManager.IsInRoleAsync(user, RoleNames.RoleTeacher).Result;
            return isTeacher;
        }
    }
}