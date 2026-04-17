using INTRANET_SSPVER.Models.Authentication;
using INTRANET_SSPVER.Models.Roles;
using INTRANET_SSPVER.Models.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace INTRANET_SSPVER.Areas.Account.Controllers
{

    [Area("Account")]
    public class AccountController : Controller
    {

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        //private readonly IUbFisicaService _areaSSPService;

        private readonly IUbFisicaService _areaSSPService;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IUbFisicaService areaSSPService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _areaSSPService = areaSSPService;
        }

        [Authorize(Roles = RolesCatalog.Sysadmin)]

        public IActionResult Dashboard() => View();

        //[HttpGet]
        //public IActionResult Register() => View();


        [HttpGet]
        public IActionResult Register()
        {
            var model = new RegisterViewModel
            {
                UbicacionFisica = _areaSSPService.ObtenerUbicacionFisica().ToList()

            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var user = new ApplicationUser
            {
                UserName = model.UserName,
                //Email = model.Email,
                NombreCompleto = model.NombreCompleto,
                IdUbicacionFisica = model.IdUbicacionFisica
            };

            var result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                //await _signInManager.SignInAsync(user, false);   EF cierra la sesión actual y abre sesión con el nuevo usuario recién creado.             
                return RedirectToAction("MostrarUsuarios", "Admin", new { area = "Admin" });
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }

            // Si falla, recarga el catálogo para que el select no quede vacío

            model.UbicacionFisica = _areaSSPService.ObtenerUbicacionFisica().ToList();

            return View(model);
        }



        [AllowAnonymous]
        //Esta acción se puede acceder sin login
        public async Task<IActionResult> Login()
        {
            //if (User.Identity.IsAuthenticated)
            //{
            //    await _signInManager.SignOutAsync();
            //}
            return View();
        }


        [HttpPost]

        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var result = await _signInManager.PasswordSignInAsync(
                model.UserName, model.Password, model.RememberMe, lockoutOnFailure: true);

            if (result.Succeeded)
            {
                // Obtener el usuario
                var user = await _userManager.FindByNameAsync(model.UserName);

                // Obtener roles/perfil
                var roles = await _userManager.GetRolesAsync(user);
                var perfil = roles.Any() ? string.Join(", ", roles) : "Sin perfil asignado";

                // Guardar mensaje en TempData
                TempData["LoginInfo"] = $"Bienvenido {user.UserName}, has iniciado sesión con el perfil: {perfil}";

                return RedirectToAction("Index", "Menu", new { area = "Inicio" });

            }

            if (result.IsLockedOut)
            {

                ModelState.AddModelError("", "Tu cuenta está bloqueada. Por favor contacta al administrador del sistema.");

                return View(model);
            }


            if (result.IsNotAllowed)
            {
                ModelState.AddModelError("", "Tu cuenta no está habilitada para iniciar sesión.");
                return View(model);
            }


            ModelState.AddModelError("", "Error en el inicio de sesión. Verifica tu usuario y contraseña.");
            return View(model);

        }


        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login");
        }


        [HttpPost]
        public async Task<IActionResult> Salir()
        {
            await _signInManager.SignOutAsync();
            //return RedirectToAction("Login");
            return RedirectToAction("Index", "Menu", new { area = "Inicio" });
        }



    }




}
