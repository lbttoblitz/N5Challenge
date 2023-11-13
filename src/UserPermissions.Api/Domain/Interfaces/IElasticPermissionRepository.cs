using UserPermissions.Api.Domain.Entities;

namespace UserPermissions.Api.Domain.Interfaces
{
    public interface IElasticPermissionRepository
    {
        Task IndexPermission(Permission permission);
    }
}
