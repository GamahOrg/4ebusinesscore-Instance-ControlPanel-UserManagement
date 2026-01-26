using AdminPanel.UserManagement.Feature.Domains.Command.DeleteDomain;
using AdminPanel.UserManagement.Feature.Domains.Command.PostDomain;
using AdminPanel.UserManagement.Feature.Domains.Command.PutDomain;
using AdminPanel.UserManagement.Feature.Domains.Query.GetDomain;
using Dtos;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RegaUserManagement.Feature.Domains.Query.GetAllDomains;
using UserManagement.Controllers;
using UserManagement.Helpers.Paginations;
using static Duende.IdentityServer.IdentityServerConstants;

namespace AdminPanel.UserManagement.Controllers.V1
{
    [ApiVersion("1.0")]
    [Authorize(LocalApi.PolicyName)]

    public class DomainController : ApiBaseController
    {
        private readonly IMediator _mediator;
        private readonly ResponseDTO _response;
        private readonly ILogger<DomainController> _logger;
        private IHttpContextAccessor _httpContextAccessor;

        public DomainController(IMediator mediator, ILogger<DomainController> logger)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _response = new ResponseDTO();
            _logger = logger;

        }


        #region Get All

        [AllowAnonymous]
        [HttpGet]
        public async Task<ResponseDTO> GeAllDomains(int pageNumber, int pageSize, string domainName, string domainNickName, bool? isActive,
            DateTime? updatedOn, string userName)
        {
            {
                var command = new GetAllDomainsQuery()
                {
                    DomainName = domainName,
                    DomainNickName = domainNickName,
                    IsActive = isActive,
                    UpdatedOn = updatedOn,
                    UserName = userName,
                    PaginatedInputModel = new PaginatedInputModel()
                    {
                        PageNumber = pageNumber,
                        PageSize = pageSize,
                    }
                };

                return await _mediator.Send(command);
            }
        }
        [HttpGet]
        [Route("{id}")]
        public async Task<ResponseDTO> GetDomain(Guid id)
        {
            return await _mediator.Send(new GetDomainQuery() { Id = id });
        }


        #endregion

        #region Post
        [HttpPost]
        public async Task<ResponseDTO> PostDomain([FromBody] PostDomainCommand command)
        {
            //_httpContextAccessor.HttpContext.Response.Headers.Add(command.Key,command.Value);

            if (!ModelState.IsValid)
            {
                _response.StatusEnum = Domain.Enums.StatusEnum.FailedToSave;
                _response.Message = "Model State Is Invalid";
                _response.Result = ModelState;
                return _response;
            }
            return await _mediator.Send(command);
        }
        #endregion

        #region Put
        [HttpPut]
        [Route("{id}")]
        public async Task<ResponseDTO> PutDomain(Guid id, [FromBody] PutDomainCommand command)
        {
            if (!ModelState.IsValid)
            {
                _response.StatusEnum = Domain.Enums.StatusEnum.FailedToSave;
                _response.Message = "Model State Is Invalid";
                _response.Result = ModelState;
                return _response;
            }
            command.Id = id;
            return await _mediator.Send(command);
        }
        #endregion

        #region Delete
        [HttpPut]
        [Route("DeleteDomain/{id}")]
        public async Task<ResponseDTO> DeleteDomain(Guid id)
        {
            return await _mediator.Send(new DeleteDomainCommand() { Id = id });
        }
        #endregion
    }
}
