using SistemaNomina.Models;
using SistemaNomina.Repositories;

namespace SistemaNomina.Services
{
    public interface IDeptEmpService
    {
        Task<List<DeptEmp>> GetAllAsync();
        Task<List<DeptEmp>> GetByEmployeeAsync(int empNo);
        Task<List<DeptEmp>> GetByDepartmentAsync(int deptNo);
        Task<bool> CreateAsync(DeptEmp deptEmp);
        Task<bool> DeleteAsync(int empNo, int deptNo, string fromDate);
    }

    public class DeptEmpService : IDeptEmpService
    {
        private readonly IRepository<DeptEmp> _repository;

        public DeptEmpService(IRepository<DeptEmp> repository)
        {
            _repository = repository;
        }

        public async Task<List<DeptEmp>> GetAllAsync()
        {
            var deptEmps = await _repository.GetAllAsync();
            return deptEmps.OrderByDescending(d => d.FromDate).ToList();
        }

        public async Task<List<DeptEmp>> GetByEmployeeAsync(int empNo)
        {
            var deptEmps = await _repository.FindAsync(d => d.EmpNo == empNo);
            return deptEmps.OrderByDescending(d => d.FromDate).ToList();
        }

        public async Task<List<DeptEmp>> GetByDepartmentAsync(int deptNo)
        {
            var deptEmps = await _repository.FindAsync(d => d.DeptNo == deptNo);
            return deptEmps.OrderByDescending(d => d.FromDate).ToList();
        }

        public async Task<bool> CreateAsync(DeptEmp deptEmp)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(deptEmp.FromDate))
                    throw new ArgumentException("La fecha de inicio es requerida.");

                await _repository.AddAsync(deptEmp);
                await _repository.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error al crear asignación: {ex.Message}", ex);
            }
        }

        public async Task<bool> DeleteAsync(int empNo, int deptNo, string fromDate)
        {
            try
            {
                var deptEmp = (await _repository.FindAsync(d =>
                    d.EmpNo == empNo && d.DeptNo == deptNo && d.FromDate == fromDate)).FirstOrDefault();

                if (deptEmp != null)
                {
                    await _repository.DeleteAsync(empNo);
                    await _repository.SaveChangesAsync();
                }

                return true;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error al eliminar asignación: {ex.Message}", ex);
            }
        }
    }
}