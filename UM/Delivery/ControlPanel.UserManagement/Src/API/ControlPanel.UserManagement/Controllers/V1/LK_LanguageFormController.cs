
using _4EPlatform_Application.Dtos.Command.LK_HomeSetting;
using _4EPlatform_Application.Dtos.Command.LK_LanguageForm;
using ControlPanel.UserManagement.Feature.Commands.DeleteLK_HomeSetting;
using ControlPanel.UserManagement.Feature.Commands.DeleteLookup;
using ControlPanel.UserManagement.Feature.Commands.PostLK_HomeSetting;
using ControlPanel.UserManagement.Feature.Commands.PostLookup;
using ControlPanel.UserManagement.Feature.Commands.PutLK_HomeSetting;
using ControlPanel.UserManagement.Feature.Commands.PutLookup;
using ControlPanel.UserManagement.Feature.Query.GetAllLK_HomeSetting;
using ControlPanel.UserManagement.Feature.Query.GetAllLooukups;
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
    public class LK_LanguageFormController : ApiBaseController
    {

        private readonly IMediator _mediator;
        //private readonly ITokenService _token;
        public Guid _loggedInUserId;

        public LK_LanguageFormController(IMediator mediator)
        {
            _mediator = mediator;

        }
        [HttpGet]
        [Route("GetAllLK_LanguageForm")]
        public async Task<ResponseDTO> GetAllLK_LanguageForm( )
        {
            return await _mediator.Send(new GetAllLK_LanguageFormQuery() {  });
        }
        #region Post
        [HttpPost]
        [Route("PostLK_LanguageForm")]
        public async Task<ResponseDTO> PostLK_LanguageForm([FromBody] PostLK_LanguageFormCommand command)
        {

            return await _mediator.Send(command);
        }
        #endregion

        #region Put

        [HttpPut("PutLK_LanguageForm/{id}")]

        public async Task<ResponseDTO> PutLK_LanguageForm(Guid id, [FromBody] PutLK_LanguageForm command)
        {
            return await _mediator.Send(new PutLK_LanguageFormCommand() { Id=id,PutLookup=command});
        }
        #endregion

        #region Delete

        [HttpDelete]
        [Route("DeleteLK_LanguageForm/{Id}")]
        public async Task<ResponseDTO> DeleteLK_LanguageForm(Guid Id)
        {
            return await _mediator.Send(new DeleteLK_LanguageFormCommand() { id = Id });
        }
        #endregion

    }

}
