using Microsoft.AspNetCore.Mvc;
using SistemaNomina.Models;
using SistemaNomina.Services;

namespace SistemaNomina.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly IEmployeeService _employeeService;

        public EmployeesController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        // GET: Employees
        public async Task<IActionResult> Index()
        {
            var employees = await _employeeService.GetAllAsync();
            return View(employees);
        }

        // GET: Employees/Create
        public IActionResult Create()
        {
            return PartialView("_EmployeeForm", new Employees());
        }

        // POST: Employees/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Employees employee)
        {
            try
            {
                await _employeeService.CreateAsync(employee);
                return Json(new { success = true, message = "Empleado creado exitosamente." });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        // GET: Employees/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var employee = await _employeeService.GetByIdAsync(id);
            if (employee == null)
                return NotFound();

            return PartialView("_EmployeeForm", employee);
        }

        // POST: Employees/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Employees employee)
        {
            if (id != employee.EmpNo)
                return BadRequest();

            try
            {
                ModelState.Remove("DeptEmps");
                ModelState.Remove("DeptManagers");
                ModelState.Remove("Titles");
                ModelState.Remove("Salaries");
                ModelState.Remove("Users");
                ModelState.Remove("LogAuditoriaSalarios");

                await _employeeService.UpdateAsync(employee);
                return Json(new { success = true, message = "Empleado actualizado exitosamente." });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        // POST: Employees/Delete/5
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _employeeService.DeleteAsync(id);
                return Json(new { success = true, message = "Empleado eliminado exitosamente." });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }
    }
}