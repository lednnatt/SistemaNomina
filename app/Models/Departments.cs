namespace SistemaNomina.Models
{
    public class Departments
    {
        public int DeptNo { get; set; }
        public string DeptName { get; set; }

        // Relaciones
        public virtual ICollection<DeptEmp> DeptEmps { get; set; }
        public virtual ICollection<DeptManager> DeptManagers { get; set; }
    }
}