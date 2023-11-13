using Mapster;
using MediatR;
using Nest;
using UserPermissions.Api.Domain.Entities;
using UserPermissions.Api.Domain.Interfaces;

namespace UserPermissions.Api.Application.Queries
{
    public class GetPermissionbyIdQry : MediatR.IRequest<GetPermissionByIdQryResponse>
    {
        public int Id { get; set; }
    }
    public class GetPermissionByIdQryResponse
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

    public class GetPermissionByIdQryHandler : IRequestHandler<GetPermissionbyIdQry, GetPermissionByIdQryResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IElasticClient _elasticClient;

        public GetPermissionByIdQryHandler(IUnitOfWork unitOfWork, IElasticClient elasticClient)
        {
            _unitOfWork = unitOfWork;
            _elasticClient = elasticClient;
        }

        public async Task<GetPermissionByIdQryResponse> Handle(GetPermissionbyIdQry request, CancellationToken cancellationToken)
        {
            var permission = await _unitOfWork.Permissions.GetByIdAsync(request.Id);

            //TODO: Realizo la persistencia en ES de un permiso solicitado. En texto que describe el challenge me pareció que el enunciado es ambiguo.
            if (permission != null)
            {
                await _elasticClient.IndexDocumentAsync(new PermissionElastic
                {
                    EmployeeName = permission.EmployeeName,
                    EmployeeLastname = permission.EmployeeLastname,
                    PermissionDate = permission.PermissionDate,
                    PermissionTypeId = permission.PermissionTypeId,
                });
            }

            return permission?.Adapt<GetPermissionByIdQryResponse>();
        }
    }
}
