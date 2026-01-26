using Domain.Enums;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models
{
	public class UserRole : IdentityUserRole<Guid>
	{
		[InverseProperty("UserRoles")]
		[Required, ForeignKey("UserId")]
		public User User { get; set; }

		[Required, ForeignKey("RoleId")]
		public Role Role { get; set; }
		public State State { get; set; }
		[Timestamp]
		public byte[] RowVersion { get; set; }

		[ForeignKey("Creator")]
		public Guid? CreatedBy { set; get; }
		public User Creator { get; set; }
		public DateTime CreatedOn { set; get; }
		public Guid? UpdatedBy { set; get; }
		public DateTime? UpdatedOn { set; get; }
	}
}
