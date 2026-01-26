using _4EPlatform_Application.Dtos.Command.ChangeProfilePicture;
using _4EPlatform_Application.Dtos.Command.ChangeUserPassword;
using _4EPlatform_Application.Dtos.Command.PostUserLoginPermission;
using _4EPlatform_Application.Dtos.Command.PutUserLoginPermission;
using _4EPlatform_Application.Feature.User.Command.PutExternalUser;
using _4EPlatform_Application.Feature.User.Query.GetRolesByUserId;
using AdminPanel.UserManagement.Feature.Users.Command.PostUserRoles;
using ControlPanel.UserManagement.Feature.Users.Command.ChangeProfilePicture;
using ControlPanel.UserManagement.Feature.Users.Command.ChangeUserPassword;
using ControlPanel.UserManagement.Feature.Users.Command.DeleteRoleFromUser;
using ControlPanel.UserManagement.Feature.Users.Command.DeleteUser;
using ControlPanel.UserManagement.Feature.Users.Command.DeleteUserRequest;
using ControlPanel.UserManagement.Feature.Users.Command.PostUserLoginPermission;
using ControlPanel.UserManagement.Feature.Users.Command.PutUserLoginPermission;
using ControlPanel.UserManagement.Feature.Users.Query.GetAllRequestsUsers;
using ControlPanel.UserManagement.Feature.Users.Query.GetAllUser;
using ControlPanel.UserManagement.Feature.Users.Query.GetBasicInformationUserByID;
using ControlPanel.UserManagement.Feature.Users.Query.GetLoggedInUser;
using ControlPanel.UserManagement.Feature.Users.Query.GetLoginPermissionByUserId;
using ControlPanel.UserManagement.Feature.Users.Query.GetRequestByUserId;
using ControlPanel.UserManagement.Feature.Users.Query.GetUser;
using ControlPanel.UserManagement.Feature.Users.Query.GetUserCount;
using ControlPanel.UserManagement.Feature.Users.Query.GetUserExperienceByUserId;
using ControlPanel.UserManagement.Feature.Users.Query.GetUsers;
using Domain.Enums;
using Dtos;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserManagement.Controllers;
using UserManagement.Helpers.Paginations;
using static Duende.IdentityServer.IdentityServerConstants;

namespace ControlPanel.UserManagement.Controllers.V1
{
    [Authorize(LocalApi.PolicyName)]
    [ApiVersion("1.0")]
    public class UserController : ApiBaseController
    {
        private readonly IMediator _mediator;
        public UserController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpGet]
        public async Task<ResponseDTO> GetAllUsers(int pageNumber, int pageSize, string userName, bool? isExpired,
             bool? isNafaz, DateTime? lastLoginTime, UserType? userType)
        {
            return await _mediator.Send(new GetAllUserQuery()
            {
                UserName = userName,
                LastLoginTime = lastLoginTime,
                IsNafaz = isNafaz == true ? true : isNafaz == false ? false : null,
                IsExpired = isExpired == true ? true : isExpired == false ? false : null,
                UserType = userType,
                PaginatedInputModel = new PaginatedInputModel()
                {
                    PageNumber = pageNumber,
                    PageSize = pageSize,
                }
            });
        }
        [HttpGet("GetUsers")]
        public async Task<ResponseDTO> GetUsers(int pageNumber, int pageSize, string userName, bool? isExpired,
             bool? isNafaz, DateTime? lastLoginTime, UserType? userType)
        {
            return await _mediator.Send(new GetUsersQuery()
            {
                UserName = userName,
                LastLoginTime = lastLoginTime,
                IsNafaz = isNafaz == true ? true : isNafaz == false ? false : null,
                IsExpired = isExpired == true ? true : isExpired == false ? false : null,
                UserType = userType,
                PaginatedInputModel = new PaginatedInputModel()
                {
                    PageNumber = pageNumber,
                    PageSize = pageSize,
                }
            });
        }
        [HttpGet]
        [Route("GetUserCount")]
        public async Task<ResponseDTO> GetUserCount()
        {
            return await _mediator.Send(new GetUserCountQuery());
        }
        //[HttpGet]
        //[Route("GetUser/{userId}")]
        //public async Task<ResponseDTO> GetUser(Guid userId)
        //{
        //	return await _mediator.Send(new GetUserQuery() { UserId = userId });
        //}

        [HttpGet]
        [Route("GetLoggedInUser")]
        public async Task<ResponseDTO> GetLoggedInUser()
        {
            return await _mediator.Send(new GetLoggedInUserQuery());
        }



        [HttpGet]
        [Route("GetLoginPermission/{id}")]
        public async Task<ResponseDTO> GetLoginPermissionByUserId(Guid id)
        {
            return await _mediator.Send(new GetLoginPermissionByUserIdQuery()
            {
                UserId = id
            });
        }

        [HttpGet]
        [Route("GetRolesByUserId/{id}")]
        public async Task<ResponseDTO> GetRolesByUserId(int pageNumber, int pageSize, Guid id)
        {
            return await _mediator.Send(new GetRolesByUserIdQuery()
            {
                UserId = id,
                PaginatedInputModel = new PaginatedInputModel()
                {
                    PageNumber = pageNumber,
                    PageSize = pageSize,
                }
            });
        }
        [HttpGet]
        [Route("GetAllRequestsUsers")]
        public async Task<ResponseDTO> GetAllRequestsUsers(int pageNumber, int pageSize, Guid id)
        {
            return await _mediator.Send(new GetAllRequestsUsersQuery()
            {
                PaginatedInputModel = new PaginatedInputModel()
                {
                    PageNumber = pageNumber,
                    PageSize = pageSize,
                }
            });
        }

        [HttpGet]
        [Route("GetRequestByUserId/{id}")]
        public async Task<ResponseDTO> GetRequestByUserId(Guid id)
        {
            return await _mediator.Send(new GetRequestByUserIdQuery()
            {
                Id = id
            });
        }
        [HttpGet]
        [Route("GetBasicInformationByUserId/{id}")]
        public async Task<ResponseDTO> GetBasicInformationByUserId(Guid id)
        {
            return await _mediator.Send(new GetBasicInformationUserByIDQuery()
            {
                UserId = id
            });
        }

        [HttpGet]
        [Route(" GetUserExperienceByUserId/{id}")]
        public async Task<ResponseDTO> GetUserExperienceByUserId(Guid id)
        {
            return await _mediator.Send(new GetUserExperienceByUserIdQuery()
            {
                UserId = id
            });
        }

        #region Post

        [HttpPost]
        [Route("PostUserRole")]
        public async Task<ResponseDTO> PostUserRole([FromBody] PostUserRolesCommand command)
        {
            return await _mediator.Send(command);
        }

        [HttpPost]
        [Route("PostUserLoginPermission/{id}")]
        public async Task<ResponseDTO> PostUserLoginPermission(Guid id, [FromBody] PostUserLoginPermissionDTO postUserLoginPermissionDTO)
        {
            return await _mediator.Send(new PostUserLoginPermissionCommand() { UserId = id, PostUserLoginPermissionDTO = postUserLoginPermissionDTO });
        }
        #endregion
        #region Put
        [HttpPut]
        [Route("ChangeUserPassword/{id}")]
        public async Task<ResponseDTO> ChangeUserPassword(Guid id, [FromBody] ChangeUserPasswordDTO changeUserPasswordDTO)
        {
            return await _mediator.Send(new ChangeUserPasswordCommand() { Id = id, ChangeUserPasswordDTO = changeUserPasswordDTO });
        }
        //[HttpPut]
        //[Route("PutUserSummary/{id}")]
        //public async Task<ResponseDTO> PutUserSummary(Guid id, [FromBody] PutUserSummaryDTO putUserSummaryDTO)
        //{
        //	return await _mediator.Send(new PutUserSummaryCommand() { PutUserSummaryDTO = putUserSummaryDTO, Id = id });
        //}


        [HttpPut]
        [Route("PutUserPermission/{id}")]
        public async Task<ResponseDTO> PutUserPermission(Guid id, [FromBody] PutLoginPermissionDTO putLoginPermissionDTO)
        {
            return await _mediator.Send(new PutUserLoginPermissionCommand() { Id = id, PutLoginPermissionDTO = putLoginPermissionDTO });
        }
        [HttpPut]
        [Route("ProfilePicture/{id}")]
        public async Task<ResponseDTO> PutProfilePicture(Guid id, [FromForm] ChangeProfilePictureDTO profilePicture)
        {
            return await _mediator.Send(new ChangeProfilePictureCommand() { Id = id, ChangeProfilePictureDTO = profilePicture });
        }
        [HttpPut]
        [Route("AllowExternalUserAccess")]
        public async Task<ResponseDTO> PutExternalUser([FromBody] PutExternalUserCommand command) => await _mediator.Send(command);

        #endregion
        #region Delete

        [HttpDelete]
        [Route("DeleteUser/{id}")]
        public async Task<ResponseDTO> DeleteUser(Guid id)
        {
            return await _mediator.Send(new DeleteUserCommand() { Id = id });
        }
        [HttpDelete]
        [Route("DeleteUserRequest/{id}")]
        public async Task<ResponseDTO> DeleteUserRequest(Guid id)
        {
            return await _mediator.Send(new DeleteUserRequestCommand() { Id = id });
        }
        [HttpDelete]
        [Route("DeleteRoleFromUser")]
        public async Task<ResponseDTO> DeleteRoleFromUser([FromBody] DeleteRoleFromUserCommand command)
        {
            return await _mediator.Send(command);
        }
        #endregion


    }
}
