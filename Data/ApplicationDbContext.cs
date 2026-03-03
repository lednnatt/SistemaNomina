using Microsoft.EntityFrameworkCore;
using SistemaNomina.Models;

namespace SistemaNomina.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Employees> Employees { get; set; }
        public DbSet<Departments> Departments { get; set; }
        public DbSet<DeptEmp> DeptEmps { get; set; }
        public DbSet<DeptManager> DeptManagers { get; set; }
        public DbSet<Titles> Titles { get; set; }
        public DbSet<Salaries> Salaries { get; set; }
        public DbSet<Users> Users { get; set; }
        public DbSet<LogAuditoriaSalarios> LogAuditoriaSalarios { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configurar Employees
            modelBuilder.Entity<Employees>()
                .HasKey(e => e.EmpNo);

            // Configurar Departments
            modelBuilder.Entity<Departments>()
                .HasKey(d => d.DeptNo);

            // Configurar DeptEmp - Llave compuesta
            modelBuilder.Entity<DeptEmp>()
                .HasKey(de => new { de.EmpNo, de.DeptNo, de.FromDate });

            modelBuilder.Entity<DeptEmp>()
                .HasOne(de => de.Employees)
                .WithMany(e => e.DeptEmps)
                .HasForeignKey(de => de.EmpNo);

            modelBuilder.Entity<DeptEmp>()
                .HasOne(de => de.Departments)
                .WithMany(d => d.DeptEmps)
                .HasForeignKey(de => de.DeptNo);

            // Configurar DeptManager - PK es EmpNo
            modelBuilder.Entity<DeptManager>()
                .HasKey(dm => dm.EmpNo);

            modelBuilder.Entity<DeptManager>()
                .HasOne(dm => dm.Employees)
                .WithMany(e => e.DeptManagers)
                .HasForeignKey(dm => dm.EmpNo);

            modelBuilder.Entity<DeptManager>()
                .HasOne(dm => dm.Departments)
                .WithMany(d => d.DeptManagers)
                .HasForeignKey(dm => dm.DeptNo);

            // Configurar Titles - Llave compuesta
            modelBuilder.Entity<Titles>()
                .HasKey(t => new { t.EmpNo, t.Title, t.FromDate });

            modelBuilder.Entity<Titles>()
                .HasOne(t => t.Employees)
                .WithMany(e => e.Titles)
                .HasForeignKey(t => t.EmpNo);

            // Configurar Salaries - Llave compuesta
            modelBuilder.Entity<Salaries>()
                .HasKey(s => new { s.EmpNo, s.FromDate });

            modelBuilder.Entity<Salaries>()
                .HasOne(s => s.Employees)
                .WithMany(e => e.Salaries)
                .HasForeignKey(s => s.EmpNo);

            // Configurar Users - 1:1 con Employees
            modelBuilder.Entity<Users>()
                .HasKey(u => u.EmpNo);

            modelBuilder.Entity<Users>()
                .HasOne(u => u.Employees)
                .WithOne(e => e.Users)
                .HasForeignKey<Users>(u => u.EmpNo);

            // Configurar LogAuditoriaSalarios
            modelBuilder.Entity<LogAuditoriaSalarios>()
                .HasKey(las => las.Id);

            modelBuilder.Entity<LogAuditoriaSalarios>()
                .HasOne(las => las.Employees)
                .WithMany(e => e.LogAuditoriaSalarios)
                .HasForeignKey(las => las.EmpNo);
        }
    }
}