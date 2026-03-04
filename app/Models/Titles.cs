namespace SistemaNomina.Models
{
    public class Titles
    {
        public int EmpNo { get; set; }
        public string Title { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }

        // Relaciones
        public virtual Employees Employees { get; set; }
    }
}