using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Enums;

namespace Domain.Models
{
	public class UserClaim : IdentityUserClaim<Guid>
	{
		public UserClaim()
		{
		}

		[Required, ForeignKey("UserId")]
		public User User { get; set; }
		public State State { get; set; }
		[Timestamp]
		public byte[] RowVersion { get; set; }
	}
}
