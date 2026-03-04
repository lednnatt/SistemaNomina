namespace SistemaNomina.Models
{
    public class Employees
    {
        public int EmpNo { get; set; }
        public string Ci { get; set; }
        public string BirthDate { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public char Gender { get; set; }
        public string HireDate { get; set; }
        public string Correo { get; set; }

        // Relaciones
        public virtual ICollection<DeptEmp> DeptEmps { get; set; }
        public virtual ICollection<DeptManager> DeptManagers { get; set; }
        public virtual ICollection<Titles> Titles { get; set; }
        public virtual ICollection<Salaries> Salaries { get; set; }
        public virtual Users Users { get; set; }
        public virtual ICollection<LogAuditoriaSalarios> LogAuditoriaSalarios { get; set; }
    }
}