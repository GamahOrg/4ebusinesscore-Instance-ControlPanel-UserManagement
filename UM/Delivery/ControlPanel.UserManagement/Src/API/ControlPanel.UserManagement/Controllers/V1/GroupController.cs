using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using AdminPanel.UserManagement.Feature.Group.Command.DeleteGroup;
using AdminPanel.UserManagement.Feature.Group.Command.DeleteUsersFromGroup;
using AdminPanel.UserManagement.Feature.Group.Command.PostGroup;
using AdminPanel.UserManagement.Feature.Group.Command.PostUsersToGroup;
using AdminPanel.UserManagement.Feature.Group.Command.PutGroup;
using AdminPanel.UserManagement.Feature.Group.Commands.PostGrouplog;
using AdminPanel.UserManagement.Feature.Group.Queries.GetAllGroupLog;
using AdminPanel.UserManagement.Feature.Group.Query.GetAllGroups;
using AdminPanel.UserManagement.Feature.Group.Query.GetAllPaginationGroups;
using static Duende.IdentityServer.IdentityServerConstants;
using UserManagement.Controllers;
using Dtos;
using AdminPanel.UserManagement.Feature.Group.Command.PostGroupToRole;

namespace AdminPanel.UserManagement.Controllers.V1
{
    [ApiVersion("1.0")]
    [Authorize(LocalApi.PolicyName)]

    public class GroupController : ApiBaseController
    {
        private readonly IMediator _mediator;
        private readonly ResponseDTO _response;
        public GroupController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _response = new ResponseDTO();
        }


        #region Get All
        [HttpGet]
        [Route("GetAllGroupLog")] 
        public async Task<ResponseDTO> GetAllGroupLog(string groupName, DateTime? createdOn, DateTime? updatedOn)
        {
            return await _mediator.Send(new GetAllGroupLogQuery());
        }
        [HttpGet]
        public async Task<ResponseDTO> GeAllGroups(string groupName,  DateTime? createdOn, DateTime? updatedOn)
        {
            {
                var command = new GetAllGroupsQuery()
                {
                    GroupName = groupName,
                    CreatedOn = createdOn,
                    UpdatedOn = updatedOn,
                    
                };

                return await _mediator.Send(command);
            }
        }
        [HttpGet]
        [Route("GetAllPaginationGroups")]
        public async Task<ResponseDTO> GetAllPaginationGroups(int pageNumber, int pageSize,string groupName,  DateTime? createdOn, DateTime? updatedOn)
        {
            {
                var command = new GetAllPaginationGroupsQuery()
                {
                    GroupName = groupName,
                    CreatedOn = createdOn,
                    UpdatedOn = updatedOn,
                    PageIndex = pageNumber,
                    PageSize = pageSize

                };

                return await _mediator.Send(command);
            }
        }
        
        //[HttpGet]
        //[Route("GetRoleGroupsWithNoId/{id}")]
        //public async Task<ResponseDTO> GetRoleGroupsWithNoId(int id)
        //{
        //    return await _mediator.Send(new GetRoleGroupsWithNoIdQuery() { Id = id });
        //}
        #endregion

        #region Post

        [HttpPost]
        [Route("PostUsersToGroup")]
        public async Task<ResponseDTO> PostGroup([FromBody] PostUsersToGroupCommand command)
        {
            
            return await _mediator.Send(command);
        }
        
        [HttpPost]
        [Route("PostGroupLog")]
        public async Task<ResponseDTO> PostGrouplog([FromBody] PostGrouplogCommand command)
        {

            return await _mediator.Send(command);
        }
       
        #endregion

        #region Put
        #endregion

        #region Delete
        [HttpDelete]
        [Route("{id}")]
        public async Task<ResponseDTO> DeleteGroup(Guid id)
        {
            return await _mediator.Send(new DeleteGroupCommand() { Id = id });
        }

        [HttpDelete]
        [Route("DeleteUsersFromGroup")]
        public async Task<ResponseDTO> DeleteGroup([FromBody] DeleteUsersFromGroupCommand command)
        {

            return await _mediator.Send(command);
        }
        #endregion
    }
}
