# N5Challenge
Challenge Backend N5

La solución cuenta con una carga inicial de información perteneciente a la tabla PermissionTypes. A continuación se detalla el identificador de permiso y la descripción de cada uno.

1 - add_users
2 - delete-users
3 - create_report
4 - edit_reports
5 - view_reports


El proyecto cuenta con 3 endpoints:

permissions/getPermissions (GET)
Devuelve una lista con los permisos cargados.

permissions/requestPermission/{id} (GET)
Solicitar un permiso previamente cargado.

permissions/modifyPermission (PUT)
Tiene la particularidad de en caso que el empleado no tenga permisos asignados los carga, sino los modifica.
A continuación se detalla el contrato
```json
{
  "employeeName": "Juan",
  "employeeLastname": "Perez",
  "permissions": [
    "add_users", "delete_users","view_reports"
  ]
}

```

