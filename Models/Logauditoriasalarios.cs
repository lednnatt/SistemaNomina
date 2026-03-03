namespace SistemaNomina.Models
{
    public class LogAuditoriaSalarios
    {
        public int Id { get; set; }
        public string Usuario { get; set; }
        public string FechaActualizacion { get; set; }
        public string DetalleCambio { get; set; }
        public long Salario { get; set; }
        public int EmpNo { get; set; }

        // Relaciones
        public virtual Employees Employees { get; set; }
    }
}