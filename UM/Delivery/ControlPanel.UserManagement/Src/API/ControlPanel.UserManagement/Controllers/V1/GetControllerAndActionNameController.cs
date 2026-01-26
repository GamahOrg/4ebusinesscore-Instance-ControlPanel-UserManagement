using _4EPlatform_Application.Feature.GetControllerAndActionName.Commands.Permession;
using AdminPanel.UserManagement.Feature.GetControllerAndActionName.Commands.GetControllerAndActionName;
using AdminPanel.UserManagement.Feature.GetControllerAndActionName.Queries.GetControllerAndActionName;
using Dtos;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AdminPanel.UserManagement.Controllers.V1
{
	
	[Route("api/[controller]")]
	[ApiController]
	public class GetControllerAndActionNameController : ControllerBase
	{
		private readonly IMediator _mediator;
		public GetControllerAndActionNameController(IMediator mediator)
		{
			_mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
		}
        [Authorize("ClientCredentialsPolicy")]
        [HttpGet("controllers-actions")]
		public async Task<ResponseDTO> GetControllerAndActions()
		{


			return await _mediator.Send(new GetControllerAndActionNameQuery());

		}
        [Authorize("ClientCredentialsPolicy")]
        [HttpPost("POSTControllerAndActions")]
		public async Task<ResponseDTO> POSTControllerAndActions()
		{


			return await _mediator.Send(new PostControllerAndActionNameCommands());

		}
        [HttpPost("AddPermession")]
        public async Task<ResponseDTO> AddPermession(AddPrmessionCommand command)
        {
            return await _mediator.Send(command);
        }
    }
}
