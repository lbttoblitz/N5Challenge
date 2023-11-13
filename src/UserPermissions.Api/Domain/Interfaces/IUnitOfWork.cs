namespace UserPermissions.Api.Domain.Interfaces
{
    public interface IUnitOfWork
    {
        IPermissionRepository Permissions { get; }
        IPermissionTypeRepository PermissionTypes { get; }
        Task<int> SaveAsync();
    }
}
