using Domain.Models;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Enums;

namespace Domain.Models
{
	public class RoleClaim : IdentityRoleClaim<Guid>
	{
		[Required, ForeignKey("RoleId")]
		public Role Role { get; set; }
		public State State { get; set; }

		[Timestamp]
		public byte[] RowVersion { get; set; }
	}
}
