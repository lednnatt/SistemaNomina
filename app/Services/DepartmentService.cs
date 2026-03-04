using SistemaNomina.Models;
using SistemaNomina.Repositories;

namespace SistemaNomina.Services
{
    public interface IDepartmentService
    {
        Task<List<Departments>> GetAllAsync();
        Task<Departments> GetByIdAsync(int id);
        Task<bool> CreateAsync(Departments department);
        Task<bool> UpdateAsync(Departments department);
        Task<bool> DeleteAsync(int id);
    }

    public class DepartmentService : IDepartmentService
    {
        private readonly IRepository<Departments> _repository;

        public DepartmentService(IRepository<Departments> repository)
        {
            _repository = repository;
        }

        public async Task<List<Departments>> GetAllAsync()
        {
            var departments = await _repository.GetAllAsync();
            return departments.ToList();
        }

        public async Task<Departments> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<bool> CreateAsync(Departments department)
        {
            try
            {
                // Validaciones
                if (string.IsNullOrWhiteSpace(department.DeptName))
                    throw new ArgumentException("El nombre del departamento es requerido.");

                var existing = await _repository.FindAsync(d => d.DeptNo == department.DeptNo);
                if (existing.Any())
                    throw new ArgumentException($"Ya existe un departamento con el código {department.DeptNo}.");

                await _repository.AddAsync(department);
                await _repository.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error al crear departamento: {ex.Message}", ex);
            }
        }

        public async Task<bool> UpdateAsync(Departments department)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(department.DeptName))
                    throw new ArgumentException("El nombre del departamento es requerido.");

                await _repository.UpdateAsync(department);
                await _repository.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error al actualizar departamento: {ex.Message}", ex);
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
                throw new InvalidOperationException($"Error al eliminar departamento: {ex.Message}", ex);
            }
        }
    }
}