using Microsoft.EntityFrameworkCore;
using UserPermissions.Api.Domain.Entities;
using UserPermissions.Api.Domain.Interfaces;
using UserPermissions.Api.Infrastructure.Data;

namespace UserPermissions.Api.Infrastructure.Repositories;

public class PermissionRepository : GenericRepository<Permission>, IPermissionRepository
{
    public PermissionRepository(PermissionContext context)
        :base(context)
    {
    }

    public override async Task<IEnumerable<Permission>> GetAllAsync()
    {
        return await _context.Permissions
            .Include(x => x.PermissionType).ToListAsync();
    }

    public override async Task<Permission> GetByIdAsync(int id)
    {
        return await _context.Permissions
            .Include(x=> x.PermissionType)
            .Where(x => x.Id == id)?.FirstOrDefaultAsync();
    }
}
