using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SistemaNomina.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "departments",
                columns: table => new
                {
                    dept_no = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    dept_name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_departments", x => x.dept_no);
                });

            migrationBuilder.CreateTable(
                name: "employees",
                columns: table => new
                {
                    emp_no = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ci = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    birth_date = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    first_name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    last_name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    gender = table.Column<string>(type: "nvarchar(1)", nullable: false),
                    hire_date = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    correo = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_employees", x => x.emp_no);
                });

            migrationBuilder.CreateTable(
                name: "dept_emp",
                columns: table => new
                {
                    emp_no = table.Column<int>(type: "int", nullable: false),
                    dept_no = table.Column<int>(type: "int", nullable: false),
                    from_date = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    to_date = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_dept_emp", x => new { x.emp_no, x.dept_no, x.from_date });
                    table.ForeignKey(
                        name: "fk_dept_emp_departments_dept_no",
                        column: x => x.dept_no,
                        principalTable: "departments",
                        principalColumn: "dept_no",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_dept_emp_employees_emp_no",
                        column: x => x.emp_no,
                        principalTable: "employees",
                        principalColumn: "emp_no",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "dept_manager",
                columns: table => new
                {
                    emp_no = table.Column<int>(type: "int", nullable: false),
                    dept_no = table.Column<int>(type: "int", nullable: false),
                    from_date = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    to_date = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_dept_manager", x => x.emp_no);
                    table.ForeignKey(
                        name: "fk_dept_manager_departments_dept_no",
                        column: x => x.dept_no,
                        principalTable: "departments",
                        principalColumn: "dept_no",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_dept_manager_employees_emp_no",
                        column: x => x.emp_no,
                        principalTable: "employees",
                        principalColumn: "emp_no",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "log_auditoria_salarios",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    usuario = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    fecha_actualizacion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    detalle_cambio = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    salario = table.Column<long>(type: "bigint", nullable: false),
                    emp_no = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_log_auditoria_salarios", x => x.id);
                    table.ForeignKey(
                        name: "fk_log_auditoria_salarios_employees_emp_no",
                        column: x => x.emp_no,
                        principalTable: "employees",
                        principalColumn: "emp_no",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "salaries",
                columns: table => new
                {
                    emp_no = table.Column<int>(type: "int", nullable: false),
                    from_date = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    salary = table.Column<long>(type: "bigint", nullable: false),
                    to_date = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_salaries", x => new { x.emp_no, x.from_date });
                    table.ForeignKey(
                        name: "fk_salaries_employees_emp_no",
                        column: x => x.emp_no,
                        principalTable: "employees",
                        principalColumn: "emp_no",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "titles",
                columns: table => new
                {
                    emp_no = table.Column<int>(type: "int", nullable: false),
                    title = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    from_date = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    to_date = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_titles", x => new { x.emp_no, x.title, x.from_date });
                    table.ForeignKey(
                        name: "fk_titles_employees_emp_no",
                        column: x => x.emp_no,
                        principalTable: "employees",
                        principalColumn: "emp_no",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    emp_no = table.Column<int>(type: "int", nullable: false),
                    usuario = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    clave = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_users", x => x.emp_no);
                    table.ForeignKey(
                        name: "fk_users_employees_emp_no",
                        column: x => x.emp_no,
                        principalTable: "employees",
                        principalColumn: "emp_no",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_dept_emp_dept_no",
                table: "dept_emp",
                column: "dept_no");

            migrationBuilder.CreateIndex(
                name: "ix_dept_manager_dept_no",
                table: "dept_manager",
                column: "dept_no");

            migrationBuilder.CreateIndex(
                name: "ix_log_auditoria_salarios_emp_no",
                table: "log_auditoria_salarios",
                column: "emp_no");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "dept_emp");

            migrationBuilder.DropTable(
                name: "dept_manager");

            migrationBuilder.DropTable(
                name: "log_auditoria_salarios");

            migrationBuilder.DropTable(
                name: "salaries");

            migrationBuilder.DropTable(
                name: "titles");

            migrationBuilder.DropTable(
                name: "users");

            migrationBuilder.DropTable(
                name: "departments");

            migrationBuilder.DropTable(
                name: "employees");
        }
    }
}
