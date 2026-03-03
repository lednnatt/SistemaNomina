using Microsoft.AspNetCore.Mvc;
using SistemaNomina.Services;

namespace SistemaNomina.Controllers
{
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        // GET: Auth/Login
        public IActionResult Login()
        {
            return View();
        }

        // POST: Auth/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(string usuario, string clave)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(usuario) || string.IsNullOrWhiteSpace(clave))
                {
                    ModelState.AddModelError(string.Empty, "Usuario y contraseña son requeridos.");
                    return View();
                }

                var employee = await _authService.AuthenticateAsync(usuario, clave);

                if (employee == null)
                {
                    ModelState.AddModelError(string.Empty, "Usuario o contraseña incorrectos.");
                    return View();
                }

                // Guardar datos en sesión
                HttpContext.Session.SetString("UsuarioLogueado", usuario);
                HttpContext.Session.SetInt32("EmpNo", employee.EmpNo);
                HttpContext.Session.SetString("NombreEmpleado", $"{employee.FirstName} {employee.LastName}");

                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"Error: {ex.Message}");
                return View();
            }
        }

        // GET: Auth/Logout
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}