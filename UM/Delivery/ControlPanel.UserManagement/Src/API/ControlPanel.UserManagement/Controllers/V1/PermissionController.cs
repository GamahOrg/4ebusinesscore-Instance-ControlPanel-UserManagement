using AdminPanel.UserManagement.Feature.Permission.Query.GetAllPermissions;
using AdminPanel.UserManagement.Feature.Permission.Query.GetPermission;
using AdminPanel.UserManagement.Feature.Permission.Command.DeletePermission;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using AdminPanel.UserManagement.Feature.Permission.Query.GetAllPaginationPermissions;
using AdminPanel.UserManagement.Feature.Permission.Command.PostPermissionToRole;
using AdminPanel.UserManagement.Feature.Permissions.Queries.GetUserPermissions;
using Microsoft.AspNetCore.Authorization;
using static Duende.IdentityServer.IdentityServerConstants;
using UserManagement.Controllers;
using Dtos;
using UserManagement.Helpers.Paginations;

namespace AdminPanel.UserManagement.Controllers.V1

{
	[ApiVersion("1.0")]
	[Authorize(LocalApi.PolicyName)]
	public class PermissionController : ApiBaseController
	{
		private readonly IMediator _mediator;
		public PermissionController(IMediator mediator)
		{
			_mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
		}


		[HttpGet]
		public async Task<ResponseDTO> GetAllPermissions()
		{
			return await _mediator.Send(new GetAllPermissionsQuery() { });
		}

		////[HttpGet]
		////[Route("GetUserPermissions")]
		////public async Task<ResponseDTO> GetUserPermissions()
		////{
		////	return await _mediator.Send(new GetUserPermissionsQuery() { });
		////}

		//[HttpGet]
		//[Route("GetAllPaginationPermissions")]
		//public async Task<ResponseDTO> GetAllPaginationPermissions(int pageNumber, int pageSize, List<string> name, List<Guid> ids, DateTime? createdOn)
		//{
		//	return await _mediator.Send(new GetAllPaginationPermissionsQuery()
		//	{
		//		Name = name,
		//		Ids = ids,
		//		CreatedOn = createdOn,
		//		PaginatedInputModel = new PaginatedInputModel()
		//		{
		//			PageNumber = pageNumber,
		//			PageSize = pageSize
		//		}
		//	});
		//}

		//[HttpGet("{id}")]
		//public async Task<ResponseDTO> GetPermissionById(Guid id)
		//{
		//	return await _mediator.Send(new GetPermissionQuery() { Id = id });
		//}


		//[HttpPost]
		//[Route("PostPermissionToRole")]
		//public async Task<ResponseDTO> PostPermissionToRole([FromBody] PostPermissionToRoleCommand command)
		//{
		//	return await _mediator.Send(command);
		//}



		//[HttpDelete]
		//[Route("{id}")]
		//public async Task<ResponseDTO> DeletePermission(Guid id)
		//{
		//	return await _mediator.Send(new DeletePermissionCommand() { Id = id });
		//}

	}
}
