using _4EPlatform_Application.Feature.UserDelegation.Command.ApprovedAndRejectDelegation;
using _4EPlatform_Application.Feature.UserDelegation.Command.DeleteUserAlternative;
using _4EPlatform_Application.Feature.UserDelegation.Command.PostUserAlternative;
using _4EPlatform_Application.Feature.UserDelegation.Command.PostUserAlternativeByVerb;
using _4EPlatform_Application.Feature.UserDelegation.Command.PutUserAlternative;
using _4EPlatform_Application.Feature.UserDelegation.NewDelegation.UserDelegation.Command;
using _4EPlatform_Application.Feature.UserDelegation.NewDelegation.UserDelegation.QueryDto;
using _4EPlatform_Application.Feature.UserDelegation.Query.GetAllAlternativesByUserId;
using _4EPlatform_Application.Feature.UserDelegation.Query.GetAllDelegationRequests;
using _4EPlatform_Application.Feature.UserDelegation.Query.GetAllDelegationsByUserId;
using _4EPlatform_Application.Feature.UserDelegation.Query.GetAlternativesById;
using AdminPanel.UserManagement.Feature.UserAlternative.Query.GetRoleForUserAlternative;
using Dtos;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserManagement.Controllers;
using UserManagement.Helpers.Paginations;
using static Duende.IdentityServer.IdentityServerConstants;

namespace AdminPanel.UserManagement.Controllers.V1
{
    [Authorize(LocalApi.PolicyName)]
    [ApiVersion("1.0")]
    public class UserDelegationController : ApiBaseController
    {
        private readonly IMediator _mediator;
        public UserDelegationController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Route("GetDelegationUserById")]
        public async Task<ResponseDTO> GetAlternativesByUserId(Guid id)
        {
            return await _mediator.Send(new GetAlternativesByUserIdQuery()
            {
                Id = id
            });
        }

        [HttpGet]
        [Route("GetAllDelegationsByUserIdWithPagination")]
        public async Task<ResponseDTO> GetAllAlternativesByUserId(int pageNumber, int pageSize)
        {
            return await _mediator.Send(new GetAllAlternativesByUserIdQuery()
            {
                PaginatedInputModel = new PaginatedInputModel()
                {
                    PageNumber = pageNumber,
                    PageSize = pageSize
                }
            });
        }
        [HttpGet]
        [Route("GetAllDelegationsByUserId")]
        public async Task<ResponseDTO> GetAllDelegationsByUserId(int pageNumber, int pageSize)
        {
            return await _mediator.Send(new GetAllDelegationsByUserIdQuery()
            {
                PaginatedInputModel = new PaginatedInputModel()
                {
                    PageNumber = pageNumber,
                    PageSize = pageSize
                }
            });
        }

        [HttpGet]
        [Route("GetRolePermissionForUserDelegation")]
        public async Task<ResponseDTO> GetRolePermissionForUserAlternative(Guid userId)
        {
            return await _mediator.Send(new GetRoleForUserAlternativeQuery()
            {
                UserId = userId
            });
        }

        [HttpPost]
        public async Task<ResponseDTO> PostUserAlternative(PostUserAlternativeCommand command)
        {
            return await _mediator.Send(command);
        }
        [HttpPost]
        [Route("PostUserAlternativeByVerb")]
        public async Task<ResponseDTO> PostUserAlternativeByVerb([FromBody] PostUserAlternativeByVerbCommand command)
        {
            return await _mediator.Send(command);
        }

        [HttpPut]
        public async Task<ResponseDTO> PutUserAlternative([FromBody] PutUserAlternativeCommand command)
        {
            return await _mediator.Send(command);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<ResponseDTO> DeleteUserAlternative(Guid id)
        {
            return await _mediator.Send(new DeleteUserAlternativeCommand { Id = id });
        }

        [HttpPut]
        [Route("ApprovedAndRejectDelegation")]
        public async Task<ResponseDTO> ApprovedAndRejectDelegation([FromBody] ApprovedAndRejectDelegationCommand command)
        {
            return await _mediator.Send(command);
        }


        [HttpGet]
        [Route("GetAllDelegationRequests")]
        public async Task<ResponseDTO> GetAllDelegationRequests(int pageNumber, int pageSize)
        {
            return await _mediator.Send(new GetAllDelegationRequestsQuery()
            {
                PaginatedInputModel = new PaginatedInputModel()
                {
                    PageNumber = pageNumber,
                    PageSize = pageSize
                }
            });
        }
        //new 
        [HttpPost("PostUserDelegationFromAdmin")]
        public async Task<ResponseDTO> PostUserDelegationFromAdmin([FromBody] PostUserDelegationFromAdminCommand command)
        {
            return await _mediator.Send(command);
        }
        [HttpPost("PostUserDelegation")]
        public async Task<ResponseDTO> PostUserDelegation([FromBody] PostUserDelegationCommand command)
        {
            return await _mediator.Send(command);
        }
        [HttpPost("UpdateUserDelegationToApproved")]
        public async Task<ResponseDTO> UpdateUserDelegationToApproved([FromBody] UpdateUserDelegationToApprovedCommand command)
        {
            return await _mediator.Send(command);
        }
        [HttpPost("UpdateUserDelegationFromApproved")]
        public async Task<ResponseDTO> UpdateUserDelegationFromApproved([FromBody] UpdateUserDelegationFromApprovedCommand command)
        {
            return await _mediator.Send(command);
        }
        [HttpPost("UpdateUserDelegationActivation")]
        public async Task<ResponseDTO> UpdateUserDelegationActivation([FromBody] UpdateUserDelegationActivationCommand command)
        {
            return await _mediator.Send(command);
        }
        [HttpGet]
        [Route("GetAllDelegationToUser")]
        public async Task<ResponseDTO> GetAllDelegationToUser(int pageNumber, int pageSize)
        {
            return await _mediator.Send(new GetAllDelegationToUserCommand()
            {
                PaginatedInputModel = new PaginatedInputModel()
                {
                    PageNumber = pageNumber,
                    PageSize = pageSize
                }
            });
        }
        [HttpGet]
        [Route("GetAllDelegationFromUser")]
        public async Task<ResponseDTO> GetAllDelegationFromUser(int pageNumber, int pageSize)
        {
            return await _mediator.Send(new GetAllDelegationFromUserCommand()
            {
                PaginatedInputModel = new PaginatedInputModel()
                {
                    PageNumber = pageNumber,
                    PageSize = pageSize
                }
            });
        }
        [HttpGet]
        [Route("GetAllDelegationToUserByUserId/{userId}")]
        public async Task<ResponseDTO> GetAllDelegationToUserByUserId(Guid userId, int pageNumber, int pageSize)
        {
            return await _mediator.Send(new GetAllDelegationToUserByUserIdCommand()
            {
                UserId = userId,
                PaginatedInputModel = new PaginatedInputModel()
                {
                    PageNumber = pageNumber,
                    PageSize = pageSize
                }
            });
        }
        [HttpGet]
        [Route("GetAllDelegationFromUserByUserId/{userId}")]
        public async Task<ResponseDTO> GetAllDelegationFromUserByUserId(Guid userId, int pageNumber, int pageSize)
        {
            return await _mediator.Send(new GetAllDelegationFromUserByUserIdCommand()
            {
                UserId = userId,
                PaginatedInputModel = new PaginatedInputModel()
                {
                    PageNumber = pageNumber,
                    PageSize = pageSize
                }
            });
        }
        [HttpPost("UpdateUserDelegation")]
        public async Task<ResponseDTO> UpdateUserDelegation([FromBody] UpdateUserDelegationCommand command)
        {
            return await _mediator.Send(command);
        }
        [HttpPost("UpdateUserDelegationFromAdmin")]
        public async Task<ResponseDTO> UpdateUserDelegationFromAdmin([FromBody] UpdateUserDelegationFromAdminCommand command)
        {
            return await _mediator.Send(command);
        }
        [HttpGet]
        [Route("GetDelegationById/{id}")]
        public async Task<ResponseDTO> GetDelegationById(Guid id)
        {
            return await _mediator.Send(new GetDelegationByIdCommand()
            {
                Id = id
            });
        }
        [HttpDelete]
        [Route("DeleteDelegation/{id}")]
        public async Task<ResponseDTO> DeleteDelegation(Guid id)
        {
            return await _mediator.Send(new DeleteDelegationCommand()
            {
                DelegeateId = id
            });
        }
    }
}
