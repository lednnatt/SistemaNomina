namespace SistemaNomina.Models
{
    public class DeptEmp
    {
        public int EmpNo { get; set; }
        public int DeptNo { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }

        // Relaciones
        public virtual Employees Employees { get; set; }
        public virtual Departments Departments { get; set; }
    }
}