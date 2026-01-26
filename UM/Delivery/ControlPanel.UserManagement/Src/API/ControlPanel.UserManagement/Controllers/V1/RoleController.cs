using _4EPlatform_Application.Feature.Roles.Queries.GetRolesByCategoryId;
using AdminPanel.UserManagement.Feature.Group.Command.PostGroupToRole;
using AdminPanel.UserManagement.Feature.Roles.Command.DeleteGruopRole;
using AdminPanel.UserManagement.Feature.Roles.Command.DeleteRole;
using AdminPanel.UserManagement.Feature.Roles.Command.DeleteUserRole;
using AdminPanel.UserManagement.Feature.Roles.Command.PostRole;
using AdminPanel.UserManagement.Feature.Roles.Command.PostUserRole;
using AdminPanel.UserManagement.Feature.Roles.Command.PutRole;
using AdminPanel.UserManagement.Feature.Roles.Queries.GetGroupsByRoleId;
using AdminPanel.UserManagement.Feature.Roles.Queries.GetRoleGroupsWithNoId;
using AdminPanel.UserManagement.Feature.Roles.Queries.GetUserByRoleId;
using AdminPanel.UserManagement.Feature.Roles.Queries.GetUsersWithNoRoleId;
using AdminPanel.UserManagement.Feature.Roles.Query.GetAllPaginationRoles;
using AdminPanel.UserManagement.Feature.Roles.Query.GetAllRoles;
using AdminPanel.UserManagement.Feature.Roles.Query.GetDetailsRoleById;
using AdminPanel.UserManagement.Feature.Roles.Query.GetRole;
using Dtos;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using UserManagement.Controllers;
using UserManagement.Helpers.Paginations;
using static Duende.IdentityServer.IdentityServerConstants;

namespace AdminPanel.UserManagement.Controllers.V1
{
    [Authorize(LocalApi.PolicyName)]
    [ApiVersion("1.0")]
    public class RoleController : ApiBaseController
    {
        private readonly IMediator _mediator;
        public RoleController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }


        [HttpGet]
        [EnableQuery]
        public async Task<ResponseDTO> GetAllRoles(int pageNumber, int pageSize, string? searchTerm)
        {
            return await _mediator.Send(new GetAllRolesQuery()
            {
                PaginatedInputModel = new PaginatedInputModel()
                {
                    PageNumber = pageNumber,
                    PageSize = pageSize
                },
                SearchTerm = searchTerm,
            });
        }
        [HttpGet]
        [Route("GetAllPaginationRoles")]
        public async Task<ResponseDTO> GetAllPaginationRoles(int pageSize, int pageNumber)
        {
            return await _mediator.Send(new GetAllPaginationRolesQuery()
            {
                PaginatedInputModel = new PaginatedInputModel()
                {
                    PageSize = pageSize,
                    PageNumber = pageNumber
                }

            });
        }

        [HttpGet("{id}")]
        public async Task<ResponseDTO> GetRoleById(Guid id)
        {
            return await _mediator.Send(new GetRoleQuery() { Id = id });
        }


        [HttpGet]
        [Route("GetUsersWithNoRoleId/{id}")]
        public async Task<ResponseDTO> GetUsersWithNoRoleId(Guid id, int PageNumber, int PageSize, string Search)
        {
            var PaginatedInputModel = new PaginatedInputModel()
            {
                PageSize = PageSize,
                PageNumber = PageNumber
            };
            return await _mediator.Send(new GetUsersWithNoRoleIdQuery() { Id = id, PaginatedInputModel = PaginatedInputModel, SearchText = Search });
        }
        [HttpGet]
        [Route("GetDetailsRoleById/{id}")]
        public async Task<ResponseDTO> GetDetailsRoleById(Guid id)
        {

            return await _mediator.Send(new GetDetailsRoleByIdQuery() { Id = id, });
        }
        [HttpGet("GetRolesByCategoryId/{categoryId}")]
        public async Task<ResponseDTO> GetRolesByCategoryId(Guid categoryId) => await _mediator.Send(new GetRolesByCategoryIdQuery(categoryId));

        #region Post
        [HttpPost]
        public async Task<ResponseDTO> PostRole([FromBody] PostRoleCommand command)
        {
            return await _mediator.Send(command);
        }

        #endregion
        #region Put

        [HttpPut]
        [Route("{id}")]
        public async Task<ResponseDTO> PutRole([FromRoute] Guid id, [FromBody] PutRoleCommand command)
        {
            // Assign id from rout to command
            command.Id = id;
            return await _mediator.Send(command);
        }
        #endregion
        #region Delete

        [HttpDelete]
        [Route("{id}")]
        public async Task<ResponseDTO> DeleteRole(Guid id)
        {
            return await _mediator.Send(new DeleteRoleCommand() { Id = id });
        }
        [HttpDelete]
        [Route("DeleteUserRole")]
        public async Task<ResponseDTO> DeleteUserRole([FromBody] DeleteUserRoleCommand command)
        {
            return await _mediator.Send(command);
        }
        [HttpDelete]
        [Route("DeleteGruopRole")]
        public async Task<ResponseDTO> DeleteGruopRole([FromBody] DeleteGruopRolerRoleCommand command)
        {
            return await _mediator.Send(command);
        }
        #endregion

        #region PostUserRole
        [HttpPost("PostUserRole")]
        public async Task<ResponseDTO> PostUserRoles([FromBody] PostUserRoleCommand command)
        {
            return await _mediator.Send(command);
        }
        #endregion
        #region PostGroupRole
        [HttpPost]
        [Route("PostGroupToRole")]
        public async Task<ResponseDTO> PostGroupToRole([FromBody] PostGroupToRoleCommand command)
        {

            return await _mediator.Send(command);
        }

        #endregion
        #region GetUserByRoleId
        [HttpGet("GetUserByRoleId/{roleId}")]
        public async Task<ResponseDTO> GetUserByRoleId(Guid roleId, string? searchText, int pageSize, int pageNumber)
        {
            return await _mediator.Send(new GetUserByRoleIdQuery()
            {
                RoleId = roleId,
                SearchText = searchText,
                PaginatedInputModel = new PaginatedInputModel()
                {
                    PageNumber = pageNumber,
                    PageSize = pageSize
                }
            });
        }
        #endregion
        #region GetGroupByRoleId
        [HttpGet("GetGroupByRoleId/{roleId}")]
        public async Task<ResponseDTO> GetGroupByRoleId(Guid roleId, int pageSize, int pageNumber, string? searchText)
        {
            return await _mediator.Send(new GetGroupsByRoleIdQuery()
            {
                RoleId = roleId,
                SearchText = searchText,
                PaginatedInputModel = new PaginatedInputModel()
                {
                    PageNumber = pageNumber,
                    PageSize = pageSize
                }
            });
        }

        [HttpGet]
        [Route("GetGroupWithNoRoleId/{id}")]
        public async Task<ResponseDTO> GetGroupWithNoRoleId(Guid id, int PageNumber, int PageSize, string Search)
        {
            var PaginatedInputModel = new PaginatedInputModel()
            {
                PageSize = PageSize,
                PageNumber = PageNumber
            };
            return await _mediator.Send(new GetRoleGroupsWithNoIdQuery() { Id = id, PaginatedInputModel = PaginatedInputModel, SearchText = Search });
        }
        #endregion
    }
}