using SistemaNomina.Models;
using SistemaNomina.Repositories;

namespace SistemaNomina.Services
{
    public interface IEmployeeService
    {
        Task<List<Employees>> GetAllAsync();
        Task<Employees> GetByIdAsync(int id);
        Task<bool> CreateAsync(Employees employee);
        Task<bool> UpdateAsync(Employees employee);
        Task<bool> DeleteAsync(int id);
    }

    public class EmployeeService : IEmployeeService
    {
        private readonly IRepository<Employees> _repository;

        public EmployeeService(IRepository<Employees> repository)
        {
            _repository = repository;
        }

        public async Task<List<Employees>> GetAllAsync()
        {
            var employees = await _repository.GetAllAsync();
            return employees.ToList();
        }

        public async Task<Employees> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<bool> CreateAsync(Employees employee)
        {
            try
            {
                // Validaciones
                if (string.IsNullOrWhiteSpace(employee.FirstName))
                    throw new ArgumentException("El nombre es requerido.");

                if (string.IsNullOrWhiteSpace(employee.LastName))
                    throw new ArgumentException("El apellido es requerido.");

                if (string.IsNullOrWhiteSpace(employee.Ci))
                    throw new ArgumentException("La cédula es requerida.");

                var existingCi = await _repository.FindAsync(e => e.Ci == employee.Ci);
                if (existingCi.Any())
                    throw new ArgumentException($"Ya existe un empleado con la cédula {employee.Ci}.");

                if (!string.IsNullOrWhiteSpace(employee.Correo) && !IsValidEmail(employee.Correo))
                    throw new ArgumentException("El correo electrónico no es válido.");

                await _repository.AddAsync(employee);
                await _repository.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error al crear empleado: {ex.Message}", ex);
            }
        }

        public async Task<bool> UpdateAsync(Employees employee)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(employee.FirstName))
                    throw new ArgumentException("El nombre es requerido.");

                if (string.IsNullOrWhiteSpace(employee.LastName))
                    throw new ArgumentException("El apellido es requerido.");

                if (!string.IsNullOrWhiteSpace(employee.Correo) && !IsValidEmail(employee.Correo))
                    throw new ArgumentException("El correo electrónico no es válido.");

                await _repository.UpdateAsync(employee);
                await _repository.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error al actualizar empleado: {ex.Message}", ex);
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                await _repository.DeleteAsync(id);
                await _repository.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error al eliminar empleado: {ex.Message}", ex);
            }
        }

        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }
    }
}