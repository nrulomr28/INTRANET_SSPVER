using INTRANET_SSPVER.Models.Authentication;
using INTRANET_SSPVER.Models.Roles;
using INTRANET_SSPVER.Models.Services.Interfaces;
using INTRANET_SSPVER.Models.ViewModels.Admin;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace INTRANET_SSPVER.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SeguridadController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IRolCatalogoService _rolCatalogoService;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public SeguridadController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IRolCatalogoService rolCatalogoService, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _rolCatalogoService = rolCatalogoService;
            _signInManager = signInManager;
        }


        [HttpGet]
        [Authorize(Roles = RolesCatalog.Sysadmin)]


        public async Task<IActionResult> MostrarUsuarios()
        {
            var users = _userManager.Users.ToList();
            var model = new ManageRolesViewModel
            {
                Users = new List<UserWithRolesViewModel>(),
                Roles = _roleManager.Roles.ToList()
            };

            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);
                model.Users.Add(new UserWithRolesViewModel
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    Email = user.Email,
                    Roles = roles.ToList(),
                    IsLockedOut = user.LockoutEnd != null && user.LockoutEnd > DateTimeOffset.Now
                });
            }

            //return View(model);
            return View("ManageRoles", model);

        }



        [HttpGet]
        [Authorize(Roles = RolesCatalog.Sysadmin)]
        public async Task<IActionResult> MostrarRol(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null) return NotFound();

            var roles = _roleManager.Roles.ToList();
            var userRoles = await _userManager.GetRolesAsync(user);

            var model = new Models.ViewModels.Admin.AssignRoleViewModel
            {
                UserId = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                Roles = roles,
                CurrentRoles = userRoles.ToList()
            };

            //return View(model);
            return View("AssignRole", model); // 👈 aquí indicas que la vista se llama AssignRole.cshtml
        }





        


        [HttpPost]
        [Authorize(Roles = RolesCatalog.Sysadmin)]
        public async Task<IActionResult> AssignRole(Models.ViewModels.Admin.AssignRoleViewModel model)
        {
            var user = await _userManager.FindByIdAsync(model.UserId);
            if (user == null) return NotFound();

            if (!string.IsNullOrEmpty(model.SelectedRole))
            {
                var result = await _userManager.AddToRoleAsync(user, model.SelectedRole);
                if (result.Succeeded)
                {
                    TempData["SuccessMessage"] = $"Rol '{model.SelectedRole}' asignado a {user.UserName}.";

                    // Refrescar cookie si el usuario modificado es el actual
                    if (user.UserName == User.Identity?.Name)
                        await _signInManager.RefreshSignInAsync(user);
                }
                else
                {
                    TempData["ErrorMessage"] = $"No se pudo asignar el rol '{model.SelectedRole}'.";
                }
            }

            return RedirectToAction("MostrarUsuarios");
        }




        
        [HttpPost]
        [Authorize(Roles = RolesCatalog.Sysadmin)]
        public async Task<IActionResult> RemoveRole(string userId, string roleName)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                TempData["ErrorMessage"] = "Usuario no encontrado.";
                return RedirectToAction("MostrarUsuarios");
            }

            if (await _userManager.IsInRoleAsync(user, roleName))
            {
                var result = await _userManager.RemoveFromRoleAsync(user, roleName);
                if (result.Succeeded)
                {
                    TempData["SuccessMessage"] = $"Rol '{roleName}' removido de {user.UserName}.";

                    //  Refrescar cookie si el usuario modificado es el actual
                    if (user.UserName == User.Identity?.Name)
                        await _signInManager.RefreshSignInAsync(user);
                }
                else
                {
                    TempData["ErrorMessage"] = $"No se pudo remover el rol '{roleName}'.";
                }
            }
            else
            {
                TempData["ErrorMessage"] = $"El usuario {user.UserName} no tiene el rol '{roleName}'.";
            }

            return RedirectToAction("MostrarUsuarios");
        }





        [HttpGet]
        [Authorize(Roles = RolesCatalog.Sysadmin)]
        public IActionResult ChangePassword(string userId)
        {
            var model = new ChangePasswordViewModel { UserId = userId };
            return View(model);
        }



        [HttpPost]
        [Authorize(Roles = RolesCatalog.Sysadmin)]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var user = await _userManager.FindByIdAsync(model.UserId);
            if (user == null)
            {
                ModelState.AddModelError("", "Usuario no encontrado.");
                return View(model);
            }

            var result = await _userManager.ChangePasswordAsync(user, model.CurrentPassword, model.NewPassword);

            if (result.Succeeded)
            {
                TempData["SuccessMessage"] = "La contraseña se cambió correctamente.";
                return RedirectToAction("MostrarUsuarios");
            }

            foreach (var error in result.Errors)
                ModelState.AddModelError("", error.Description);

            return View(model);

        }



        [HttpGet]
        [Authorize(Roles = RolesCatalog.Sysadmin)]
        public IActionResult ResetPasswordAdmin(string userId)
        {
            var model = new ResetPasswordViewModel { UserId = userId };
            return View(model);

        }

        [HttpPost]
        [Authorize(Roles = RolesCatalog.Sysadmin)]
        public async Task<IActionResult> ResetPasswordAdmin(ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var user = await _userManager.FindByIdAsync(model.UserId);
            if (user == null)
            {
                ModelState.AddModelError("", "Usuario no encontrado.");
                return View(model);
            }

            // Generar token de reseteo
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);

            // Resetear directamente con el token
            var result = await _userManager.ResetPasswordAsync(user, token, model.NewPassword);

            if (result.Succeeded)
            {
                TempData["SuccessMessage"] = "La contraseña se cambió correctamente.";
                return RedirectToAction("MostrarUsuarios");
            }

            foreach (var error in result.Errors)
                ModelState.AddModelError("", error.Description);

            return View(model);

        }


        [HttpPost]
        public async Task<IActionResult> BloquearUsuario(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null) return NotFound();

            user.LockoutEnabled = true;
            user.LockoutEnd = DateTimeOffset.MaxValue;
            await _userManager.UpdateAsync(user);

            return RedirectToAction("MostrarUsuarios");
        }

        [HttpPost]
        public async Task<IActionResult> DesbloquearUsuario(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null) return NotFound();

            user.LockoutEnd = null;
            await _userManager.UpdateAsync(user);

            return RedirectToAction("MostrarUsuarios");
        }


    }



}
