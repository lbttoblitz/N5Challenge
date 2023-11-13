﻿
namespace UserPermissions.Api.Domain.Entities;

public class Permission:BaseEntity
{
    /// <summary>
    /// Employee forename
    /// </summary>
    public string EmployeeName  { get; set; }
    
    /// <summary>
    /// Employee surname
    /// </summary>
    public string EmployeeLastname { get; set; }

    /// <summary>
    /// Identifier of permission types
    /// </summary>
    public int PermissionTypeId { get; set; }

    /// <summary>
    /// Permission type
    /// </summary>
    public PermissionType PermissionType { get; set; }

    /// <summary>
    /// Permission granted on date
    /// </summary>
    public DateTime PermissionDate { get; set; } = DateTime.Now;
}
