namespace SistemaNomina.Models
{
    public class Users
    {
        public int EmpNo { get; set; }
        public string Usuario { get; set; }
        public string Clave { get; set; }

        // Relaciones
        public virtual Employees Employees { get; set; }
    }
}