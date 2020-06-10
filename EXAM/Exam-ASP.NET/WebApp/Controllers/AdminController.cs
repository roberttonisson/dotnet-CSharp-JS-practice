﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
 using DAL.App.EF.Helpers;
 using Domain;
using Domain.Identity;
using Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
 using WebApp.ViewModels;

 namespace WebApp.Controllers
{
    [Authorize(Roles = RoleNames.RoleAdmin)]
    public class AdminController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<AppRole> _roleManager;
        private readonly SignInManager<AppUser> _signInManager;

        public AdminController(UserManager<AppUser> userManager, RoleManager<AppRole> roleInManager, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _roleManager = roleInManager;
            _signInManager = signInManager;
        }

        //GET users
        public async Task<IActionResult> Users()
        {
            return View(await _userManager.Users.ToListAsync());
        }

        //GET Edit user
        public async Task<IActionResult> UserEdit(string? id)
        {
            if (id == null)
            {
                return NotFound();
            }    
            
            var vm = new UserEditCreateViewModel();
            
            vm.AppUser = await _userManager.FindByIdAsync(id);
            IList<AppRole> allRoles = await _roleManager.Roles.ToListAsync();
            
            foreach (var role in await _userManager.GetRolesAsync(vm.AppUser))
            {
                vm.RoleId = role;
            }
            
            vm.RoleSelectList = new SelectList(allRoles, nameof(AppRole.Id), nameof(AppRole.Name), nameof(vm.RoleId));
            
            return View(vm);
        }

        //Post Edit user
        [HttpPost]
        public async Task<IActionResult> UserEdit(string id, UserEditCreateViewModel vm)
        {
            var user = await _userManager.FindByIdAsync(id);
            var roleName = (await _roleManager.FindByIdAsync(vm.RoleId)).Name;

            if (ModelState.IsValid)
            {
                foreach (var role in await _userManager.GetRolesAsync(user))
                { 
                    await _userManager.RemoveFromRoleAsync(user, role);    
                }

                await _userManager.AddToRoleAsync(user, roleName);
                await _userManager.UpdateAsync(user);
                user.Email = vm.AppUser.Email;
                user.FirstName = vm.AppUser.FirstName;
                user.LastName = vm.AppUser.LastName;
                
                await _userManager.UpdateAsync(user);
                
                return RedirectToAction(nameof(Users));
            }

            return View(vm);
        }

        public async Task<IActionResult> UserDelete(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            if (user.Id == User.UserGuidId())
            {
                await _signInManager.SignOutAsync();
            }
            
            await _userManager.DeleteAsync(user);
            return RedirectToAction(nameof(Users));
        }


        //GET Roles
        public async Task<IActionResult> Roles()
        {
            return View(await _roleManager.Roles.ToListAsync());
        }

        //GET Create role
        public IActionResult RoleCreate()
        {
            return View();
        }

        //Post Create role
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RoleCreate(AppRole appRole)
        {
            if (ModelState.IsValid)
            {
                await _roleManager.CreateAsync(appRole);
                return RedirectToAction(nameof(Roles));
            }

            return View(appRole);
        }

        //GET Edit role
        public async Task<IActionResult> RoleEdit(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);

            if (role == null)
            {
                return BadRequest();
            }

            return View(role);
        }

        //Post Edit role
        [HttpPost]
        public async Task<IActionResult> RoleEdit(string id, AppRole appRole)
        {
            if (Guid.Parse(id) != appRole.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var role = await _roleManager.FindByIdAsync(id);
                role.Name = appRole.Name;
                await _roleManager.UpdateAsync(role);
                return RedirectToAction(nameof(Roles));
            }

            return View(appRole);
        }

        public async Task<IActionResult> RoleDelete(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            if (role == null)
            {
                return NotFound();
            }

            await _roleManager.DeleteAsync(role);
            return RedirectToAction(nameof(Roles));
        }
    }
}