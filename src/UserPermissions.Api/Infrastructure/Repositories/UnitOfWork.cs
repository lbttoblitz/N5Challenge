using UserPermissions.Api.Domain.Interfaces;
using UserPermissions.Api.Infrastructure.Data;

namespace UserPermissions.Api.Infrastructure.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly PermissionContext _context;
    private IPermissionRepository _permissions;
    private IPermissionTypeRepository _permissionTypes;

    public IPermissionRepository Permissions
    {
        get
        {
            if (_permissions == null)
                _permissions = new PermissionRepository(_context);

            return _permissions;
        }
    }

    public IPermissionTypeRepository PermissionTypes
    {
        get
        {
            if (_permissionTypes == null)
                _permissionTypes = new PermissionTypeRepository(_context);

            return _permissionTypes;
        }
    }

    public UnitOfWork(PermissionContext context)
    {
        _context = context;
    }

    public async Task<int> SaveAsync()
    {
        return await _context.SaveChangesAsync();
    }
}
