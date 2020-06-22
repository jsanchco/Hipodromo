# Previo

 - Vistar esta opagina para configurar SQL: https://www.dundas.com/support/learning/documentation/installation/how-to-enable-sql-server-authentication
 - Crear un usuario: user-> admin password-> admin  Luego restart SQL modificar en Security->Logins->admin [entrar en admin y en ServerRoles añadir el check de sysadmin]
 - Restart SQL

# Migración

- Establecer como proyecto de inicio en donde tengamos instanciado Codere.SBGOnline.Hipodromo.Domain (cadena de conexión). En mi caso Codere.SBGOnline.Hipodromo.API
- En la 'Consola del Administrador de Paquetes' establecer como 'Proyecto predeterminado' en donde tengamos instanciados los repositorios. En mi caso Codere.SBGOnline.Infrastructure
- Ejecutamos en Power-Shell 'add-migration First_Migration -Context Codere.SBGOnline.Infrastructure.EFContextSQL'
- Se creará en Codere.SBGOnline.Domain una carpeta (Migrations) en donde se creará la migración
- Establecemos el entorno en el que se va a hacer la migración : $env:ASPNETCORE_ENVIRONMENT='Development'  or  $env:ASPNETCORE_ENVIRONMENT='Production'
- Construimos la base de datos en donde haya indicado la cadena de conexión ejecutando en Power-Shell 'Update-Database -Context Codere.SBGOnline.Infrastructure.EFContextSQL'
- Si queremos eliminar la Migración, ejecutamos en Power-Shell 'remove-migration -Context Codere.SBGOnline.Infrastructure.EFContextSQL'
