using Microsoft.AspNetCore.Mvc;
using SistemaNomina.Models;
using SistemaNomina.Services;

namespace SistemaNomina.Controllers
{
    public class DepartmentsController : Controller
    {
        private readonly IDepartmentService _departmentService;

        public DepartmentsController(IDepartmentService departmentService)
        {
            _departmentService = departmentService;
        }

        // GET: Departments
        public async Task<IActionResult> Index()
        {
            var departments = await _departmentService.GetAllAsync();
            return View(departments);
        }

        // GET: Departments/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var department = await _departmentService.GetByIdAsync(id);
            if (department == null)
                return NotFound();

            return View(department);
        }

        // GET: Departments/Create
        public IActionResult Create()
        {

            return PartialView("_DepartmentForm", new Departments());
        }

        // POST: Departments/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Departments department)
        {
            try
            {
                await _departmentService.CreateAsync(department);
                return Json(new { success = true, message = "Departamento creado exitosamente." });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        // GET: Departments/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var department = await _departmentService.GetByIdAsync(id);
            if (department == null)
                return NotFound();

            return PartialView("_DepartmentForm", department);
        }

        // POST: Departments/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Departments department)
        {
            if (id != department.DeptNo)
                return BadRequest();

            try
            {
                ModelState.Remove("DeptEmps");
                ModelState.Remove("DeptManagers");

                await _departmentService.UpdateAsync(department);
                return Json(new { success = true, message = "Departamento actualizado exitosamente." });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        // POST: Departments/Delete/5
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _departmentService.DeleteAsync(id);
                return Json(new { success = true, message = "Departamento eliminado exitosamente." });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }
    }
}