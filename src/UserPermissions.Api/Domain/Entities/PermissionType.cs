
namespace UserPermissions.Api.Domain.Entities;

public class PermissionType : BaseEntity
{
    /// <summary>
    /// Permission description
    /// </summary>
    public string Description { get; set; }

    /// <summary>
    /// Permissions
    /// </summary>
    public ICollection<Permission> Permissions { get; set; }
}
