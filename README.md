# Sistema de Gestión de Nóminas

## TÍTULO.
Aplicación ASP.NET Core MVC para gestión de empleados, departamentos y asignaciones con autenticación básica.

## TECNOLOGÍAS.
- **Backend**: ASP.NET Core 9.0 MVC
- **BD**: SQL Server 2022
- **ORM**: Entity Framework Core
- **Frontend**: Bootstrap 5

## MÓDULOS IMPLEMENTADOS.
1. **Autenticación** - Login básico (usuario: admin, contraseña: admin)
2. **Empleados** - CRUD completo (crear, editar, ver, eliminar)
3. **Departamentos** - CRUD completo
4. **Asignaciones** - Asignar empleados a departamentos con fechas de vigencia

## CARACTERÍSTICAS. 
- Patrón Repository → Service → Controller
- Inyección de dependencias
- Validaciones de negocio
- Manejo de errores

## INSTALACIÓN.
1. Clonar repositorio
2. Abrir en Visual Studio 
3. En la terminal, ejecutar:
   -> dotnet restore
   -> dotnet ef database update
   -> dotnet run

## USUARIO DE PRUEBA, PARA ACCEDER AL SISTEMA.
- Usuario: `admin`
- Contraseña: `admin`

## ESTRUCTURA DEL PROYECTO.
```
/app          - Código ASP.NET Core MVC
/db           - Migraciones de EF Core
```

## FUNCIONALIDADES FUTURAS.
- Gestión de Salarios con Auditoría
- Reportes PDF/Excel
- Protección de rutas por sesión
- Roles y permisos

## AUTOR
Leonardo Antonio Arévalo - 2026