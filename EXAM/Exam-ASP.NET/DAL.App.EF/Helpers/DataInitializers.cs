using System;
using System.Collections.Generic;
using System.Linq;
using Domain;
using Domain.Identity;
using Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Helpers
{
    public class DataInitializers
    {
        public static void MigrateDatabase(AppDbContext context)
        {
            context.Database.Migrate();
        }
        public static bool DeleteDatabase(AppDbContext context)
        {
            return context.Database.EnsureDeleted();
        }

        public static void SeedIdentity(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
        {
            
            var roleNames = new[] {RoleNames.RoleStudent, RoleNames.RoleTeacher, RoleNames.RoleAdmin, RoleNames.RoleGuest};
            foreach (var roleName in roleNames)
            {

                var role = roleManager.FindByNameAsync(roleName).Result;
                if (role == null)
                {
                    role = new AppRole();
                    role.Name = roleName;
                    var result = roleManager.CreateAsync(role).Result;
                    if (!result.Succeeded)
                    {
                        throw new ApplicationException("Role creation failed!");
                    }
                }
                
            }
            
            var admin = new AppUser()
            {
                Email = "rotoni@rotoni.ee",
                UserName = "rotoni@rotoni.ee",
                FirstName = "ro",
                LastName = "toni"
            };

            var user = userManager.FindByNameAsync(admin.UserName).Result;
            if (user == null)
            {
                var result = userManager.CreateAsync(admin, "Abc123@").Result;
                var addRole = userManager.AddToRoleAsync(admin, RoleNames.RoleAdmin).Result;
                if (!result.Succeeded)
                {
                    throw new ApplicationException("User creation failed!");

                }
            }

            var teachers = new List<AppUser>()
            {
                new AppUser()
                {
                    Email = "t1@t1.com",
                    UserName = "t1@t1.com",
                    FirstName = "t1first",
                    LastName = "t1last",
                },
                new AppUser()
                {
                    Email = "t2@t2.com",
                    UserName = "t2@t2.com",
                    FirstName = "t2first",
                    LastName = "t2last",
                }
                
            };
            var students = new List<AppUser>()
            {
                new AppUser()
                {
                    Email = "s1@s1.com",
                    UserName = "s1@s1.com",
                    FirstName = "s1First",
                    LastName = "s1Last",
                },
                new AppUser()
                {
                    Email = "s2@s2.com",
                    UserName = "s2@s2.com",
                    FirstName = "s2First",
                    LastName = "s2Last",
                },
                new AppUser()
                {
                    Email = "s3@s3.com",
                    UserName = "s3@s3.com",
                    FirstName = "s3First",
                    LastName = "s3Last",
                }
                
            };

            foreach (var t in teachers)
            {
                var x = userManager.FindByNameAsync(t.UserName).Result;
                if (x == null)
                {
                    var t1 = userManager.CreateAsync(t, "Abc123@").Result;
                    var addTeacherRole = userManager.AddToRoleAsync(t, RoleNames.RoleTeacher).Result;
                }
            }

            foreach (var s in students)
            {
                var x = userManager.FindByNameAsync(s.UserName).Result;
                if (x == null)
                {
                    var s1 = userManager.CreateAsync(s, "Abc123@").Result;
                    var addStudentRole = userManager.AddToRoleAsync(s, RoleNames.RoleStudent).Result;
                }
            }
            

        }
        
        public static void SeedData(AppDbContext context)
        {
            
        }
    }
}