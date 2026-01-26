using MediatR;
using Microsoft.AspNetCore.Mvc;
using AdminPanel.UserManagement.Feature.Account.Command.ChangePassword;
using AdminPanel.UserManagement.Feature.Account.Command.ForgetPassword;
using AdminPanel.UserManagement.Feature.Account.Command.MobileVerification;
using AdminPanel.UserManagement.Feature.Account.Commands.Login;
using static Duende.IdentityServer.IdentityServerConstants;
using AdminPanel.UserManagement.Feature.Account.Command.LogOut;
using UserManagement.Controllers;
using Dtos;
using AdminPanel.UserManagement.Feature.Account.Command.ResetPassword;
using Microsoft.AspNetCore.Authorization;
using AdminPanel.UserManagement.Feature.Account.Commands.LoginV2;
using AdminPanel.UserManagement.Feature.Account.Commands.Integrations;

namespace AdminPanel.UserManagement.Controllers.V1
{
	[ApiVersion("1.0")]
	[Authorize(LocalApi.PolicyName)]
	public class AccountController : ApiBaseController
	{
		private readonly IMediator _mediator;
        public Guid _loggedInUserId;
        private readonly ILogger<AccountController> _logger;

        public AccountController(IMediator mediator, IHttpContextAccessor _httpContextAccessor, ILogger<AccountController> logger)
		{
			_mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
			_logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
        [HttpPost]
        [Route("Logout")]
        public async Task<ResponseDTO> Logout()
        {
            var command = new LogOutCommand();
            return await _mediator.Send(command);
        }
        [AllowAnonymous]
		[HttpPost]
		[Route("Login")]
		public async Task<ResponseDTO> Login([FromBody] LoginQuery command)
		{
			_logger.LogInformation("start login");
			return await _mediator.Send(command);
            _logger.LogInformation("end login");

        }

        [AllowAnonymous]
        [HttpPost]
        [Route("Integration")]
        public async Task<ResponseDTO> Integration([FromBody] IntegrationsCommand command)
        {
            return await _mediator.Send(command);

        }

        [HttpPost]
		[AllowAnonymous]
		[Route("ForgetPassword")]
		public async Task<ResponseDTO> ForgetPassword([FromBody] ForgetPasswordCommand command)
		{
			return await _mediator.Send(command);
		}

		[HttpPost]
		[AllowAnonymous]
		[Route("MobileVerification")]
		public async Task<ResponseDTO> MobileVerification([FromBody] MobileVerificationCommand command)
		{
			return await _mediator.Send(command);
		}

		[HttpPost]
		[AllowAnonymous]
		[Route("ResetPassword")]
		public async Task<ResponseDTO> ResetPassword([FromBody] ResetPasswordCommand command)
		{
			return await _mediator.Send(command);
		}

		#region Put Block
		[HttpPut]
		[Route("ChangePassword")]
		public async Task<ResponseDTO> PutPassword([FromBody] ChangePasswordCommand command)
		{
			return await _mediator.Send(command);
		}
        [AllowAnonymous]
        [HttpPost]
        [Route("Auth")]
        public async Task<ResponseDTO> Auth([FromBody] AuthQuery command)
        {
			try
			{
				_logger.LogInformation("start Auth");
				return await _mediator.Send(command);
				_logger.LogInformation("end Auth");
			}
			catch (Exception ex) { throw; }
        }
        #endregion
    }
}
