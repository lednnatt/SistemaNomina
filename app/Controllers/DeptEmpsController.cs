using Microsoft.AspNetCore.Mvc;
using SistemaNomina.Models;
using SistemaNomina.Services;

namespace SistemaNomina.Controllers
{
    public class DeptEmpsController : Controller
    {
        private readonly IDeptEmpService _deptEmpService;
        private readonly IEmployeeService _employeeService;
        private readonly IDepartmentService _departmentService;

        public DeptEmpsController(IDeptEmpService deptEmpService, IEmployeeService employeeService, IDepartmentService departmentService)
        {
            _deptEmpService = deptEmpService;
            _employeeService = employeeService;
            _departmentService = departmentService;
        }

        // GET: DeptEmps
        public async Task<IActionResult> Index()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("UsuarioLogueado")))
                return RedirectToAction("Login", "Auth");

            var deptEmps = await _deptEmpService.GetAllAsync();
            return View(deptEmps);
        }

        // GET: DeptEmps/ByEmployee/5
        public async Task<IActionResult> ByEmployee(int empNo)
        {
            var employee = await _employeeService.GetByIdAsync(empNo);
            if (employee == null)
                return NotFound();

            var deptEmps = await _deptEmpService.GetByEmployeeAsync(empNo);
            ViewBag.Employee = employee;
            return View(deptEmps);
        }

        // GET: DeptEmps/Create
        public async Task<IActionResult> Create(int empNo)
        {
            var employee = await _employeeService.GetByIdAsync(empNo);
            if (employee == null)
                return NotFound();

            var departments = await _departmentService.GetAllAsync();
            ViewBag.Employee = employee;
            ViewBag.Departments = departments;
            return PartialView("_DeptEmpForm", new { EmpNo = empNo });
        }

        // POST: DeptEmps/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int empNo, int deptNo, string fromDate)
        {
            try
            {
                ModelState.Remove("Employees");
                ModelState.Remove("Departments");

                var deptEmp = new DeptEmp
                {
                    EmpNo = empNo,
                    DeptNo = deptNo,
                    FromDate = fromDate,
                    ToDate = ""
                };

                await _deptEmpService.CreateAsync(deptEmp);
                return Json(new { success = true, message = "Asignación creada exitosamente." });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        // POST: DeptEmps/Delete
        [HttpPost]
        public async Task<IActionResult> Delete(int empNo, int deptNo, string fromDate)
        {
            try
            {
                await _deptEmpService.DeleteAsync(empNo, deptNo, fromDate);
                return Json(new { success = true, message = "Asignación eliminada exitosamente." });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }
    }
}