using _4EPlatform_Application.Dtos.Command.UserProfileCommandDTO;
using _4EPlatform_Application.Feature.UserProfile.Command.PatchLanguageForm;
using _4EPlatform_Application.Feature.UserProfile.Query.GetBasicInformation;
using _4EPlatform_Application.Feature.UserProfile.Query.GetHomeSettingForUser;
using _4EPlatform_Application.Feature.UserProfile.Query.GetLanguageForm;
using _4EPlatform_Application.Feature.UserProfile.Query.GetUserExperienceById;
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
    public class UserProfileController : ApiBaseController
    {
        private readonly IMediator _mediator;
        public UserProfileController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }
       
        [HttpPost]
        [Route("PostUserExperience")]
        public async Task<ResponseDTO> PostUserExperience([FromForm] PostUserExperienceCommand command)
        {
            return await _mediator.Send(command);
        }
        [HttpPut]
        [Route("PutUserExperience/{Id}")]
        public async Task<ResponseDTO> PutUserExperience(Guid Id, [FromForm] PutUserExperienceDTO command)
        {

            return await _mediator.Send(new PutUserExperienceCommand
            {
                Id = Id,
                putUserExperience = command
            });
        }
        [HttpDelete]
        [Route("DeleteUserExperience/{id}")]
        public async Task<ResponseDTO> DeleteUserExperience(Guid id)
        {
            return await _mediator.Send(new DeleteuserExperienceCommand() { Id = id });
        }
        [HttpPut]
        [Route("PutUserBasicInformation")]
        public async Task<ResponseDTO> PutUserBasicInformation([FromForm] PutUserBasicInformationCommand PutUserBasicInformationCommand)
        {


            return await _mediator.Send(PutUserBasicInformationCommand);
        }
        [HttpGet("GetAllHomeSettingForUser")]
        public async Task<ResponseDTO> GetAllHomeSettingForUser()
        {
            return await _mediator.Send(new GetHomeSettingQuery() { });
        }

        [HttpPost]
        [Route("PostHomeSetting")]
        public async Task<ResponseDTO> PostHomeSetting([FromBody] PostHomeSettingForUserCommand putHomeSettingForUser)
        {
            return await _mediator.Send(putHomeSettingForUser);
     }
        [HttpGet("GetAllLanguageFormForUser")]
        public async Task<ResponseDTO> GetAllLanguageFormForUser()
        {
            return await _mediator.Send(new GetLanguageFormQuery() { });
        }

        [HttpPut]
        [Route("PutLanguageForm")]
        public async Task<ResponseDTO> PutLanguageForm([FromForm] PutLanguageFormForUserCommand putHomeSettingForUser)
        {
            return await _mediator.Send(putHomeSettingForUser);
        }

        [HttpPatch]
        [Route("PatchLanguageForm")]
        public async Task<ResponseDTO> PatchLanguageForm([FromForm] PatchLanguageFormCommand patchHomeSettingForUser)
        {
            return await _mediator.Send(patchHomeSettingForUser);
        }

        [HttpGet("GetBasicInformation")]
        public async Task<ResponseDTO> GetBasicInformation()
        {
            return await _mediator.Send(new GetBasicInformationQuery() { });
        }
        [HttpGet("GetUserExperienceById/{Id}")]
        public async Task<ResponseDTO> GetUserExperienceById(Guid Id)
        {
            return await _mediator.Send(new GetUserExperienceByIdQuery() { Id=Id});
        }

    }
}
