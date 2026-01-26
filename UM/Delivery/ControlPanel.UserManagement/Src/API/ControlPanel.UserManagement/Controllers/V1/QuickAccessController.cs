using MediatR;
using Microsoft.AspNetCore.Mvc;
using AdminPanel.UserManagement.Feature.QuickAccess.Commands.ChangeFlagQuickAccess;
using Microsoft.AspNetCore.Authorization;
using static Duende.IdentityServer.IdentityServerConstants;
using UserManagement.Controllers;
using Dtos;

namespace AdminPanel.UserManagement.Controllers.V1
{
    [ApiVersion("1.0")]
    [Authorize(LocalApi.PolicyName)]

    public class QuickAccessController : ApiBaseController
    {
        private readonly ResponseDTO _responseDTO;
        private readonly IMediator _mediator;
        public QuickAccessController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        #region Put
        [HttpPut]
        [Route("{id}")]
        public async Task<ResponseDTO> ChangeFlagQuickAccess(Guid id)
        {
            return await _mediator.Send(new ChangeFlagQuickAccessCommand() { Id = id});
        }
        #endregion
    }
}