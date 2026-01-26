using _4EPlatform_Application.Feature.Lookup.Command.JobDegree.DeleteJobDegree;
using _4EPlatform_Application.Feature.Lookup.Command.JobDegree.PutLookup;
using _4EPlatform_Application.Feature.Lookup.Command.JobNames.DeleteJobNames;
using _4EPlatform_Application.Feature.Lookup.Command.JobNames.PostLookup;
using _4EPlatform_Application.Feature.Lookup.Command.JobNames.PutLookup;
using _4EPlatform_Application.Feature.Lookup.Command.JobRole.DeleteJobRole;
using _4EPlatform_Application.Feature.Lookup.Command.JobRole.PutJobRole;
using _4EPlatform_Application.Feature.Lookup.Command.RoleCategory.DeleteRoleCategory;
using _4EPlatform_Application.Feature.Lookup.Command.RoleCategory.PostRoleCategory;
using _4EPlatform_Application.Feature.Lookup.Command.RoleCategory.PutRoleCategory;
using _4EPlatform_Application.Feature.Lookup.Query.JobDegree;
using _4EPlatform_Application.Feature.Lookup.Query.JobName;
using _4EPlatform_Application.Feature.Lookup.Query.JobRole;
using _4EPlatform_Application.Feature.Lookup.Query.RoleCategory;
using Dtos;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UserManagement.Controllers;
using static Duende.IdentityServer.IdentityServerConstants;

namespace ControlPanel.UserManagement.Controllers.V1
{
    [Route("api/[controller]")]
    [Authorize(LocalApi.PolicyName)]
    [ApiController]
    [Authorize(LocalApi.PolicyName)]
    public class LookupController : ApiBaseController
    {
        private readonly IMediator _mediator;
        private readonly ResponseDTO _response;
        public LookupController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _response = new ResponseDTO();
        }


        #region JobDegree
        [HttpGet]
        [Route("GetAllJobDegree")]
        public async Task<ResponseDTO> GetAllJobDegree()
        {
            return await _mediator.Send(new GetAllJobDegreeQuery() { });
        }
        #region Post
        [HttpPost]
        [Route("AddJobDegree")]
        public async Task<ResponseDTO> PostLookup([FromBody] PostJobDegreeCommand command)
        {

            return await _mediator.Send(command);
        }
        #endregion

        #region Put

        [HttpPut("PutJobDegree")]

        public async Task<ResponseDTO> PutJobDegree(Guid id, [FromBody] PutJobDegreeCommand command)
        {
            return await _mediator.Send(new PutJobDegreeCommand() { Id = id, PutLookup = command.PutLookup });
        }
        #endregion

        #region Delete

        [HttpDelete]
        [Route(("DeleteJobDegree"))]
        public async Task<ResponseDTO> DeleteJobDegree(Guid Id)
        {
            return await _mediator.Send(new DeleteJobDegreeCommand() { id = Id });
        }
        #endregion
        #endregion

        #region JobRole
        [HttpGet]
        [Route("GetAllJobRole")]
        public async Task<ResponseDTO> GetAllJobRole()
        {
            return await _mediator.Send(new GetAllJobRoleQuery() { });
        }
        #region Post
        [HttpPost]
        [Route("AddJobRole")]
        public async Task<ResponseDTO> PostJobRole([FromBody] PostJobRoleCommand command)
        {

            return await _mediator.Send(command);
        }
        #endregion

        #region Put

        [HttpPut("PutJobRole")]

        public async Task<ResponseDTO> PutJobRole(Guid id, [FromBody] PutJobRoleCommand command)
        {
            return await _mediator.Send(new PutJobRoleCommand() { Id = id, PutLookup = command.PutLookup });
        }
        #endregion

        #region Delete

        [HttpDelete]
        [Route(("DeleteJobRole"))]
        public async Task<ResponseDTO> DeleteJobRole(Guid Id)
        {
            return await _mediator.Send(new DeleteLookupCommand() { id = Id });
        }
        #endregion
        #endregion

        #region JobNames
        [HttpGet]
        [Route("GetAllJobNames")]
        public async Task<ResponseDTO> GetAllJobNames()
        {
            return await _mediator.Send(new GetAllJobNameQuery() { });
        }
        #region Post
        [HttpPost]
        [Route("AddJobNames")]
        public async Task<ResponseDTO> PostJobNames([FromBody] PostJobNameCommand command)
        {

            return await _mediator.Send(command);
        }
        #endregion

        #region Put

        [HttpPut("PutJobNames")]

        public async Task<ResponseDTO> PutJobNames(Guid id, [FromBody] PutJobNamesCommand command)
        {
            return await _mediator.Send(new PutJobNamesCommand() { Id = id, PutLookup = command.PutLookup });
        }
        #endregion

        #region Delete

        [HttpDelete]
        [Route("DeleteJobName")]
        public async Task<ResponseDTO> DeleteJobNames(Guid Id)
        {
            return await _mediator.Send(new DeleteJobNamesCommand() { id = Id });
        }
        #endregion

        #endregion

        #region RoleCategory
        [HttpGet]
        [Route("GetAllRoleCategory")]
        public async Task<ResponseDTO> GetAllRoleCategory()
        {
            return await _mediator.Send(new GetAllRoleCategoryQuery() { });
        }
        #region Post
        [HttpPost]
        [Route("AddRoleCategory")]
        public async Task<ResponseDTO> PostRoleCategory([FromBody] PostRoleCategoryCommand command)
        {

            return await _mediator.Send(command);
        }
        #endregion

        #region Put

        [HttpPut("PutRoleCategory/{id}")]

        public async Task<ResponseDTO> PutRoleCategory(Guid id, [FromBody] PutRoleCategoryCommand command)
        {
            return await _mediator.Send(new PutRoleCategoryCommand() { Id = id, PutLookup = command.PutLookup });
        }
        #endregion

        #region Delete

        [HttpDelete]
        [Route("DeleteRoleCategory/{Id}")]
        public async Task<ResponseDTO> DeleteRoleCategory(Guid Id)
        {
            return await _mediator.Send(new DeleteRoleCategoryCommand() { id = Id });
        }
        #endregion
        #endregion
    }
}


