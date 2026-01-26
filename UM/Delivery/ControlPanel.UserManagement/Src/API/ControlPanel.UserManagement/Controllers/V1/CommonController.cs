using _4EPlatform_Application.Feature.Statistics.Query;
using Dtos;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserManagement.Controllers;
using static Duende.IdentityServer.IdentityServerConstants;

namespace ControlPanel.UserManagement.Controllers.V1
{
    [ApiVersion("1.0")]
    [Authorize(LocalApi.PolicyName)]
    public class CommonController : ApiBaseController
    {
        private readonly IMediator _mediator;
        public CommonController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet]
        [Route("GetStatisticsCount")]
        public async Task<ResponseDTO> GetUserCount()
        {
            return await _mediator.Send(new GetStatisticsCountQuery());
        }
    }
}
