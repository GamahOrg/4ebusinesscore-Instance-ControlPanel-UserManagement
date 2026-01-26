using Dtos;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserManagement.Feature.Users.Command.MursalatAuthentication;
using static Duende.IdentityServer.IdentityServerConstants;

namespace UserManagement.Controllers.V1
{
    [ApiVersion("1.0")]
    [Authorize(LocalApi.PolicyName)]
    public class IntegrationController : ApiBaseController
    {

        private readonly IMediator _mediator;
        public IntegrationController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpPost]
        [Route("MursalatAuthentication")]
        public async Task<ResponseDTO> MursalatAuthentication([FromBody] MursalatAuthenticationCommand command)
        {
            return await _mediator.Send(command);
        }
    }
}
