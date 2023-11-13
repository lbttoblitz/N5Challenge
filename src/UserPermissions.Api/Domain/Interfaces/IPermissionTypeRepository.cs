using UserPermissions.Api.Domain.Entities;

namespace UserPermissions.Api.Domain.Interfaces
{
    public interface IPermissionTypeRepository:IGenericRepository<PermissionType>
    {
        Task<PermissionType> GetByName(string name);
    }
}
