
using _4EPlatform_Application.Dtos.Command.LK_HomeSetting;
using ControlPanel.UserManagement.Feature.Commands.DeleteLK_HomeSetting;
using ControlPanel.UserManagement.Feature.Commands.PostLK_HomeSetting;
using ControlPanel.UserManagement.Feature.Commands.PutLK_HomeSetting;
using ControlPanel.UserManagement.Feature.Query.GetAllLK_HomeSetting;
using Dtos;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UserManagement.Controllers;
using static Duende.IdentityServer.IdentityServerConstants;

namespace ControlPanel.UserManagement.Controllers.V1
{
    [Authorize(LocalApi.PolicyName)]
    [ApiVersion("1.0")]
    [ApiController]
    public class LK_HomeSettingController : ApiBaseController
    {

        private readonly IMediator _mediator;
        //private readonly ITokenService _token;
        public Guid _loggedInUserId;

        public LK_HomeSettingController(IMediator mediator)
        {
            _mediator = mediator;

        }
        [HttpGet]
        [Route("GetAllLK_HomeSetting")]
        public async Task<ResponseDTO> GetAllLK_HomeSetting( )
        {
            return await _mediator.Send(new GetAllLK_HomeSettingQuery() {  });
        }
        #region Post
        [HttpPost]
        [Route("PostLK_HomeSetting")]
        public async Task<ResponseDTO> PostLK_HomeSetting([FromBody] PostLK_HomeSettingCommand command)
        {

            return await _mediator.Send(command);
        }
        #endregion

        #region Put

        [HttpPut("PutLK_HomeSetting/{id}")]

        public async Task<ResponseDTO> PutLK_HomeSetting(Guid id, [FromBody] PutLK_HomeSetting command)
        {
            return await _mediator.Send(new PutLK_HomeSettingCommand() { Id=id,PutLookup=command});
        }
        #endregion

        #region Delete

        [HttpDelete]
        [Route("DeleteLK_HomeSetting/{Id}")]
        public async Task<ResponseDTO> DeleteLK_HomeSetting(Guid Id)
        {
            return await _mediator.Send(new DeleteLK_HomeSettingCommand() { id = Id });
        }
        #endregion

    }

}
