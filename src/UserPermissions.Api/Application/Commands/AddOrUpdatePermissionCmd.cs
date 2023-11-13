using MediatR;
using UserPermissions.Api.Domain.Entities;
using UserPermissions.Api.Domain.Interfaces;

namespace UserPermissions.Api.Application.Commands;

public class AddOrUpdatePermissionCmd : IRequest
{
    public string EmployeeName { get; set; }
    public string EmployeeLastname { get; set; }
    public string[] Permissions { get; set; }
}

public class AddOrUpdatePermissionCmdHandler : IRequestHandler<AddOrUpdatePermissionCmd>
{
    private readonly IUnitOfWork _unitOfWork;

    public AddOrUpdatePermissionCmdHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Unit> Handle(AddOrUpdatePermissionCmd cmd, CancellationToken cancellationToken)
    {
        var permissions =  await _unitOfWork.Permissions.GetAllAsync();
        
        var permissionsByEmployee = permissions.Where(x => x.EmployeeName == cmd.EmployeeName
        && x.EmployeeLastname == cmd.EmployeeLastname).ToList();
        
        if(permissions.Count()> 0)
            _unitOfWork.Permissions.RemoveRange(permissionsByEmployee);

        foreach (var permission in cmd.Permissions)
        {
            var p = new Permission
            {
                EmployeeLastname = cmd.EmployeeLastname,
                EmployeeName = cmd.EmployeeName
            };

            var permissionType = await _unitOfWork.PermissionTypes.GetByName(permission);
            if (permissionType is null)
                throw new Exception($"No se pudo asignar un permiso con el nombre {permission}");
            
            p.PermissionType = permissionType;
            p.PermissionTypeId = p.PermissionType.Id;
            _unitOfWork.Permissions.Add(p);
        }

        var res = await _unitOfWork.SaveAsync();

        return (res > 0) ? Unit.Value : throw new Exception("no se pudieron cargar los permisos");
    }
}
