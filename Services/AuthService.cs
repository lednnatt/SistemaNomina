using SistemaNomina.Models;
using SistemaNomina.Repositories;
using System.Security.Cryptography;
using System.Text;

namespace SistemaNomina.Services
{
    public interface IAuthService
    {
        Task<Employees?> AuthenticateAsync(string usuario, string clave);
        string HashPassword(string password);
        bool VerifyPassword(string password, string hash);
    }

    public class AuthService : IAuthService
    {
        private readonly IRepository<Users> _userRepository;
        private readonly IRepository<Employees> _employeeRepository;

        public AuthService(IRepository<Users> userRepository, IRepository<Employees> employeeRepository)
        {
            _userRepository = userRepository;
            _employeeRepository = employeeRepository;
        }

        public async Task<Employees?> AuthenticateAsync(string usuario, string clave)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(usuario) || string.IsNullOrWhiteSpace(clave))
                    return null;

                // Buscar el usuario en la tabla Users
                var users = await _userRepository.FindAsync(u => u.Usuario == usuario);
                var user = users.FirstOrDefault();

                if (user == null)
                    return null;

                // Verificar contraseña (comparación simple para MVP)
                if (user.Clave != clave)
                    return null;

                // Obtener datos del empleado
                var employee = await _employeeRepository.GetByIdAsync(user.EmpNo);
                return employee;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error en autenticación: {ex.Message}", ex);
            }
        }

        public string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return Convert.ToBase64String(hashedBytes);
            }
        }

        public bool VerifyPassword(string password, string hash)
        {
            var hashOfInput = HashPassword(password);
            return hashOfInput == hash;
        }
    }
}