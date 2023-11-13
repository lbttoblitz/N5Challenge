using Microsoft.AspNetCore.Mvc;
using MediatR;
using UserPermissions.Api.Application.Commands;
using UserPermissions.Api.Application.Queries;

namespace UserPermissions.Api.Application.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PermissionsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PermissionsController(IMediator mediator)
        {
            _mediator = mediator;
        }
        
        [HttpGet("getPermissions")]
        public async Task<IActionResult> GetPermissions()
        {
            var response = await _mediator.Send(new GetPermissionsQry { });
            
            return Ok(response);
        }

        [HttpGet("requestPermission/{id}")]
        public async Task<IActionResult> RequestPermission([FromRoute] int id)
        {
            var response = await _mediator.Send(new GetPermissionbyIdQry { Id = id });
            
            return Ok(response);
        }

        [HttpPut("modifyPermission")]
        public async Task<IActionResult> ModifyPermission(AddOrUpdatePermissionCmd cmd)
        {
            var response = await _mediator.Send(cmd);

            return Ok(response);
        }
    }
}
