using MediatR;
using UserPermissions.Api.Domain.Interfaces;
namespace UserPermissions.Api.Application.Queries;

public class GetPermissionsQry : IRequest<List<GetPermissionsQryResponse>>
{
}

public class GetPermissionsQryResponse
{
    public int Id { get; set; }
    public string EmployeeName { get; set; }
    public string EmployeeLastname { get; set; }
    public GetPermissionTypeResponse PermissionType { get; set; }
    public DateTime PermissionDate { get; set; }

    public class GetPermissionTypeResponse
    {
        public int Id { get; set; }
        public string Description { get; set; }
    }
}

public class GetPermissionsQryHandler : IRequestHandler<GetPermissionsQry, List<GetPermissionsQryResponse>>
{
    private readonly IUnitOfWork _unitOfWork;
    public GetPermissionsQryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<List<GetPermissionsQryResponse>> Handle(GetPermissionsQry request, CancellationToken cancellationToken)
    {
        var permissions = await _unitOfWork.Permissions.GetAllAsync();
        
            return permissions.Select(x => new GetPermissionsQryResponse
        {
            Id = x.Id,
            EmployeeName = x.EmployeeName,
            EmployeeLastname = x.EmployeeLastname,
            PermissionType = new GetPermissionsQryResponse.GetPermissionTypeResponse
            {
                Id = x.PermissionType.Id,
                Description = x.PermissionType.Description
            }
            ,PermissionDate = x.PermissionDate
        }).ToList();
    }
}
