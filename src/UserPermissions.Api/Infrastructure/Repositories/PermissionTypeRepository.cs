using Microsoft.EntityFrameworkCore;
using UserPermissions.Api.Domain.Entities;
using UserPermissions.Api.Domain.Interfaces;
using UserPermissions.Api.Infrastructure.Data;

namespace UserPermissions.Api.Infrastructure.Repositories;

public class PermissionTypeRepository : GenericRepository<PermissionType>, IPermissionTypeRepository
{
    public PermissionTypeRepository(PermissionContext context)
        : base(context)
    {
    }

    public async Task<PermissionType> GetByName(string name)
    {
        return await _context.PermissionTypes.Where(z => Convert.ToString(z.Description) ==name.ToLower())?.FirstOrDefaultAsync();
    }
}
