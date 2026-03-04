namespace SistemaNomina.Models
{
    public class Salaries
    {
        public int EmpNo { get; set; }
        public long Salary { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }

        // Relaciones
        public virtual Employees Employees { get; set; }
    }
}
